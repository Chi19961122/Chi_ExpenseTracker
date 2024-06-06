using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chi_ExpenseTracker_Service.Models.Jwt
{
    /// <summary>
    /// JWT Login 的 ViewModel
    /// </summary>
    public class JwtLoginDto
    {
        /// <summary>
        /// 帳號
        /// </summary>
        public string? Account { get; set; }

        /// <summary>
        /// 密碼
        /// </summary>
        public string? Password { get; set; }
    }
}
