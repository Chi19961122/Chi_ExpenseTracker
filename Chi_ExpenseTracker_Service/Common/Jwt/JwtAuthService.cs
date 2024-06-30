using Chi_ExpenseTracker_Service.Common.User;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;
using Chi_ExpenseTracker_Service.Models.Api;
using Chi_ExpenseTracker_Service.Models.Jwt;
using Chi_ExpenseTracker_Repesitory.Configuration;
using Chi_ExpenseTracker_Service.Models.Api.Enums;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Chi_ExpenseTracker_Repesitory.Models;
using Microsoft.EntityFrameworkCore;
using Chi_ExpenseTracker_Repesitory.Database.Repository;
using System.Text;
using Chi_ExpenseTracker_Service.Models.User;
using Azure.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Chi_ExpenseTracker_Service.Common.Jwt
{
    public class JwtAuthService : IJwtAuthService
    {
        private readonly IUserService? _userService;
        private readonly IHttpContextAccessor? _httpContextAccessor;
        private readonly IUserRepository _userRepository;
        public JwtAuthService(IServiceProvider serviceProvider)
        {
            _userService = serviceProvider.GetService<IUserService>();
            _httpContextAccessor = serviceProvider.GetService<IHttpContextAccessor>();
            _userRepository = serviceProvider.GetService<IUserRepository>();
        }

        /// <summary>
        /// 註冊服務
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ApiResponseModel Register(RegisterDto newUser)
        {
            var result = new ApiResponseModel();

            //檢核用戶是否存在
            UserEntity userData = _userService.GetUserByAccount(newUser.Email);

            if (userData != null)
            {
                result.ApiResult(ApiCodeEnum.DuplicatedData);
                result.Msg = "用戶已經存在";
                return result;
            }

            //使用BCrypt.Net將密碼進行雜湊處理
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(newUser.Password);

            //新增用戶
            var newUserEntity = new UserEntity() 
            {
                UserName = newUser.UserName,
                Password = passwordHash,
                Email = newUser.Email,
                Role = "User",
            };

            _userRepository.Add(newUserEntity);

            ///回傳ApiRes
            result.ApiResult(ApiCodeEnum.Success);
            result.Data = newUser;
            return result;
        }
            
        /// <summary>
        /// 網頁登入驗證
        /// </summary>
        /// <param name="jwtLoginViewModel"></param>
        /// <returns></returns>
        public ApiResponseModel Login(JwtLoginDto jwtLoginViewModel)
        {
            var result = new ApiResponseModel();

            var user = _userService.GetUserByAccount(jwtLoginViewModel.Account);
            
            ///要回傳的Dto
            var resultData = new JwtTokenDto()
            {
                Account = jwtLoginViewModel.Account
            };

            ///驗證密碼
            if (BCrypt.Net.BCrypt.Verify(jwtLoginViewModel.Password, user.Password))
            {
                var refreshToken = GenerateToken(jwtLoginViewModel, 1440);

                ///更新DB的RefreshToken
                user.RefreshToken = refreshToken;
                _userRepository.UpdateRefreshToken(user);

                resultData.UserId = user.UserId;
                resultData.Token = GenerateToken(jwtLoginViewModel, 720);
                resultData.Refresh = refreshToken;
                resultData.Role = user.Role;

                result.ApiResult(ApiCodeEnum.Success);
            }
            else
            {
                result.ApiResult(ApiCodeEnum.InputError);
                result.Msg = "Wrong Username Or Password";
            }

            ///回傳ApiRes
            result.Data = resultData;
            return result;
        }

        private byte[] GenerateRandomKey(int keySize)
        {
            var key = new byte[keySize / 8]; // 轉換為位元組
            new RNGCryptoServiceProvider().GetBytes(key); // 使用 CSPRNG 來填充密鑰
            return key;
        }

        /// <summary>
        /// 產生Jwt Token
        /// </summary>
        /// <param name="jwtLoginViewModel"></param>
        /// <param name="expireMinutes"></param>
        /// <returns></returns>
        private string GenerateToken(JwtLoginDto jwtLoginViewModel, int expireMinutes)
        {
            var issuer = AppSettings.JwtConfig.Issuer;
            var signKey = AppSettings.JwtConfig.IssuerSigningKey;

            var user = _userService.GetUserByAccount(jwtLoginViewModel.Account);

            if (user == null)
            {
                return string.Empty;
            }

            // 設定要加入到 JWT Token 中的聲明資訊(Claims)
            var claims = new List<Claim>() {
                new Claim(JwtRegisteredClaimNames.Iss, issuer),
                new Claim(JwtRegisteredClaimNames.Sub, jwtLoginViewModel.Account!), // User.Identity.Name
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // JWT ID
                //new Claim(ClaimTypes.Role, user.Role)
            };

            var userClaimsIdentity = new ClaimsIdentity(claims);

            // 建立一組對稱式加密的金鑰，主要用於 JWT 簽章之用
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signKey));

            // HmacSha256 有要求必須要大於 128 bits，所以 key 不能太短，至少要 16 字元以上
            // https://stackoverflow.com/questions/47279947/idx10603-the-algorithm-hs256-requires-the-securitykey-keysize-to-be-greater
            //byte[] signKeyBytes = GenerateRandomKey(256); // 生成 256 位的隨機密鑰
            //SymmetricSecurityKey securityKey = new SymmetricSecurityKey(signKeyBytes); // 將字節序列轉換為 SymmetricSecurityKey
            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256Signature);

            // 建立 SecurityTokenDescriptor
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Issuer = issuer,
                //Audience = issuer, // 由於你的 API 受眾通常沒有區分特別對象，因此通常不太需要設定，也不太需要驗證
                //NotBefore = DateTime.Now, // 預設值就是 DateTime.Now
                //IssuedAt = DateTime.Now, // 預設值就是 DateTime.Now
                Subject = userClaimsIdentity,
                Expires = DateTime.Now.AddMinutes(expireMinutes), // 設定 Token 有效期限
                SigningCredentials = signingCredentials
            };

            // 產出所需要的 JWT securityToken 物件，並取得序列化後的 Token 結果(字串格式)
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            string serializeToken = tokenHandler.WriteToken(securityToken);

            return serializeToken;
        }

        /// <summary>
        /// 刷新JWT Token
        /// </summary>
        /// <param name="tokenViewModel"></param>
        /// <returns></returns>
        public JwtTokenDto RefreashToken(JwtTokenDto tokenDto)
        {
            var user = _userService.GetUserByAccount(tokenDto.Account);

            ///要回傳的Dto
            var jwtLoginDto = new JwtLoginDto()
            {
                Account = tokenDto.Account
            };

            ///驗證Token
            var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token == user.RefreshToken)
            {
                var refreshToken = GenerateToken(jwtLoginDto, 1440);

                tokenDto.Token = GenerateToken(jwtLoginDto, 720);
                tokenDto.Refresh = refreshToken;

                user.RefreshToken = refreshToken;
                _userRepository.UpdateRefreshToken(user);
                
            }
            return tokenDto;
        }

    }
}
