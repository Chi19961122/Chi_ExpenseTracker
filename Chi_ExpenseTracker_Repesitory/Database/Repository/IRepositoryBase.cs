using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Chi_ExpenseTracker_Repesitory.Database.Repository
{
    /// <summary>
    /// 基本CRUD介面
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepositoryBase<T>
    {
        /// <summary>
        /// 篩選資料
        /// </summary>
        /// <returns></returns>
        T? Find(Expression<Func<T, bool>>? trueAndCondition = null);

        /// <summary>
        /// 篩選資料清單
        /// </summary>
        /// <returns></returns>
        IQueryable<T> Filter(Expression<Func<T, bool>>? trueAndCondition = null);

        /// <summary>
        /// 新增資料
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Add(T entity);

        /// <summary>
        /// 更新資料
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Update(T entity);

        /// <summary>
        /// 刪除資料
        /// </summary>
        /// <param name="trueAndCondition"></param>
        /// <returns></returns>
        int DeleteByFilter(Expression<Func<T, bool>>? trueAndCondition = null);
    }
}
