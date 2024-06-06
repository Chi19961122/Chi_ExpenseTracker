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

namespace Chi_ExpenseTracker_Service.Common.Jwt
{
    public class JwtAuthService : IJwtAuthService
    {
        private readonly IUserService? _userService;
        private readonly IHttpContextAccessor? _httpContextAccessor;
        private readonly IUserRepository _userRepository;
        public JwtAuthService(IUserService userService,
            IHttpContextAccessor httpContextAccessor,
            IUserRepository userRepository)
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
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

            //新增用戶
            var newUserEntity = new UserEntity() 
            {
                UserName = newUser.UserName,
                Password = newUser.Password,
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
            UserEntity user = _userService.GetUserByAccount(jwtLoginViewModel.Account);
            
            ///要回傳的Dto
            var resultData = new JwtTokenDto()
            {
                Account = jwtLoginViewModel.Account
            };

            ///驗證密碼
            if (user?.Password == jwtLoginViewModel.Password)
            {
                var refreshToken = GenerateToken(jwtLoginViewModel, 1440);

                ///更新DB的RefreshToken
                user.RefreshToken = refreshToken;
                _userRepository.UpdateRefreshToken(user);

                resultData.Token = GenerateToken(jwtLoginViewModel, 720);
                resultData.Refresh = refreshToken;
                resultData.Role = user.Role;
            }

            ///回傳ApiRes
            var result = new ApiResponseModel();
            result.ApiResult(ApiCodeEnum.Success);
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

            // Claims
            List<Claim> claims = new() {
                new Claim(JwtRegisteredClaimNames.Iss, issuer),
                new Claim(JwtRegisteredClaimNames.Sub, jwtLoginViewModel.Account!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                //new Claim(ClaimTypes.Role, user.Role)
            };

            ClaimsIdentity userClaimsIdentity = new(claims);

            // SigningCredentials
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(signKey));
            //byte[] signKeyBytes = GenerateRandomKey(256); // 生成 256 位的隨機密鑰
            //SymmetricSecurityKey securityKey = new SymmetricSecurityKey(signKeyBytes); // 將字節序列轉換為 SymmetricSecurityKey
            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256Signature);

            // SecurityTokenDescriptor
            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Issuer = issuer,
                Subject = userClaimsIdentity,
                Expires = DateTime.Now.AddMinutes(expireMinutes), // 設定 Token 有效期限
                SigningCredentials = signingCredentials
            };

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
            UserEntity user = _userService.GetUserByAccount(tokenDto.Account);

            ///要回傳的Dto
            JwtLoginDto jwtLoginDto = new()
            {
                Account = tokenDto.Account
            };

            ///驗證Token
            string token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

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
