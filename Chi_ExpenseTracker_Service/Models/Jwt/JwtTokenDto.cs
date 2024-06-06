using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chi_ExpenseTracker_Service.Models.Jwt
{
    /// <summary>
    /// JWT TOKEN 的 ViewModel
    /// </summary>
    public class JwtTokenDto
    {
        /// <summary>
        /// 帳號
        /// </summary>
        public string? Account { set; get; }

        /// <summary>
        /// 角色
        /// </summary>
        public string? Role { set; get; }

        /// <summary>
        /// 授權Token字串
        /// </summary>
        public string? Token { get; set; }

        /// <summary>
        /// 刷新Token字串
        /// </summary>
        public string? Refresh { get; set; }
    }
}
