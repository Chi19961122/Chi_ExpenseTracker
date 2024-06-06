using Chi_ExpenseTracker_Repesitory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Chi_ExpenseTracker_Repesitory.Database.Repository
{
    /// <summary>
    /// 使用者資料表操做介面
    /// </summary>
    public interface IUserRepository : IRepositoryBase<UserEntity>
    {
        /// <summary>
        /// 客戶設定檔by業主編號
        /// </summary>
        /// <param name="custCode">業主編號</param>
        /// <returns>回傳客戶設定檔</returns>
        int UpdateRefreshToken(UserEntity user);
    }
}
