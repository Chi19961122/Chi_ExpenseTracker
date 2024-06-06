using Chi_ExpenseTracker_Repesitory.Models;
using Chi_ExpenseTracker_Service.Models.Api;
using Chi_ExpenseTracker_Service.Models.Jwt;
using Chi_ExpenseTracker_Service.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chi_ExpenseTracker_Service.Common.Jwt
{
    public interface IJwtAuthService
    {
        ApiResponseModel Login(JwtLoginDto jwtLoginViewModel);

        JwtTokenDto RefreashToken(JwtTokenDto tokenDto);

        ApiResponseModel Register(RegisterDto newUser);
    }
}
