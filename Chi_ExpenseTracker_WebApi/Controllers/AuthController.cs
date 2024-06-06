using Chi_ExpenseTracker_Repesitory.Models;
using Chi_ExpenseTracker_Service.Common.Jwt;
using Chi_ExpenseTracker_Service.Models.Api;
using Chi_ExpenseTracker_Service.Models.Jwt;
using Chi_ExpenseTracker_Service.Models.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Chi_ExpenseTracker_WebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AuthController : Controller
    {
        private readonly IJwtAuthService _jwtAuth;

        /// <summary>
        /// JWT服務載入
        /// </summary>
        public AuthController(IJwtAuthService jwtAuthService)
        {
            _jwtAuth = jwtAuthService;
        }

        /// <summary>
        /// 註冊服務
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResponseModel Register(RegisterDto newUser)
        {
            var result = _jwtAuth.Register(newUser);

            return new ApiResponseModel
            {
                Code = result.Code,
                Msg = result.Msg,
                Data = result.Data
            };
        }

        /// <summary>
        /// 產生JWT TOKEN
        /// </summary>
        /// <param name="jwtLoginViewModel">登入資料</param>
        /// <returns></returns>
        [HttpPost]
        public ApiResponseModel Login([FromBody] JwtLoginDto jwtLoginViewModel)
        {
            var result = _jwtAuth.Login(jwtLoginViewModel);

            return new ApiResponseModel
            {
                Code = result.Code,
                Data = result.Data
            };
        }

        /// <summary>
        /// 驗證Token
        /// </summary>
        /// <returns>Token資料</returns>
        [HttpPost]
        public IActionResult RefreshToken([FromBody] JwtTokenDto tokenViewModel)
        {
            JwtTokenDto result = _jwtAuth.RefreashToken(tokenViewModel);

            return Ok(result);
        }
    }
}
