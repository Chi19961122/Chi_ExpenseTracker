
using Microsoft.Extensions.Configuration;

namespace Chi_ExpenseTracker_Repesitory.Configuration
{
    /// <summary>
    /// 參數設定
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// 參數設定
        /// </summary>
        /// <param name="configuration"></param>
        public AppSettings(IConfiguration configuration)
        {
            configuration.Bind(this);
        }

        /// <summary>
        /// 連線字串
        /// </summary>
        public static Connectionstrings? Connectionstrings { get; set; }

        /// <summary>
        /// JWT驗證
        /// </summary>
        public static Jwtconfig? JwtConfig { get; set; }
    }

    /// <summary>
    /// 連線字串
    /// </summary>
    public class Connectionstrings
    {
        /// <summary>
        /// 資料串接資料庫
        /// </summary>
        public string? ChiConn { get; set; }
        /// <summary>
        /// 資料串接資料庫
        /// </summary>
        public string? Company { get; set; }
    }

    /// <summary>
    /// JWT驗證
    /// </summary>
    public class Jwtconfig
    {
        /// <summary>
        /// 發行者
        /// </summary>
        public string? Issuer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? Audience { get; set; }

        /// <summary>
        /// 密鑰
        /// </summary>
        public string? IssuerSigningKey { get; set; }

        /// <summary>
        /// 過期設定
        /// </summary>
        public string? AccessTokenExpiresMinutes { get; set; }
    }

}
