using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using System.Data.Common;
using System.Linq.Expressions;
using System.Linq;
using static Dapper.SqlMapper;
using EFCore.BulkExtensions;

namespace Chi_ExpenseTracker_Repesitory.Database
{
    public class DbBase<Tentity, TDb> where Tentity : class where TDb : DbContext
    {
        /// <summary>
        /// DB
        /// </summary>
        protected readonly TDb _dbContext;

        public DbBase(TDb dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            var connectionString = dbContext.Database.GetConnectionString();
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ApplicationException($"DB Name={dbContext.GetType().Name}，查無連線字串!");
            }

            _dbContext = dbContext;
        }

        #region Dapper(執行SQL)
        /// <summary>
        /// 執行Select命令
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        public IEnumerable<Tentity> SqlQuery(string? sql, object? param = null)
        {
            if (string.IsNullOrEmpty(sql))
            {
                throw new ArgumentException("SQL query cannot be null or empty", nameof(sql));
            }

            using DbConnection connection = new SqlConnection(_dbContext.Database.GetConnectionString());
            return connection.Query<Tentity>(sql, param);
        }

        public IEnumerable<T> SqlQuery<T>(string? sql, object? param = null)
        {
            if (_dbContext.Database.CurrentTransaction != null)
            {
                var tran = _dbContext.Database.CurrentTransaction.GetDbTransaction();
                var conn = tran.Connection;
                return conn.Query<T>(sql, param, transaction: tran);
            }
            else
            {
                using DbConnection connection = new SqlConnection(_dbContext.Database.GetConnectionString());
                return connection.Query<T>(sql, param);
            }
        }

        /// <summary>
        /// 執行Insert、Update、Delete命令
        /// </summary>
        /// <param name="func"></param>
        public int ExecuteSqlCommand(string? sql, object? param = null)
        {
            if (string.IsNullOrEmpty(sql))
            {
                throw new ArgumentException("SQL query cannot be null or empty", nameof(sql));
            }

            using DbConnection connection = new SqlConnection(_dbContext.Database.GetConnectionString());
            return connection.Execute(sql, param);
    }
        #endregion

        #region EF Core
        /// <summary>
        /// 執行新增
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns>新增筆數</returns>
        public int Add(Tentity entity)
        {
            CheckEntityIsNull(entity);

            EntityState state = _dbContext.Entry(entity).State;

            if (state == EntityState.Detached)
            {
                _dbContext.Entry(entity).State = EntityState.Added;
            }

            return _dbContext.SaveChanges();
        }

        /// <summary>
        /// 執行更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns>更新筆數</returns>
        public int Update(Tentity entity)
        {
            CheckEntityIsNull(entity);

            EntityState state = _dbContext.Entry(entity).State;

            if (state == EntityState.Detached)
            {
                _dbContext.Set<Tentity>().Attach(entity);
            }

            _dbContext.Entry(entity).State = EntityState.Modified;

            return _dbContext.SaveChanges();
        }

        /// <summary>
        /// 執行刪除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns>刪除筆數</returns>
        public int Delete(Tentity entity)
        {
            CheckEntityIsNull(entity);

            _dbContext.Entry(entity).State = EntityState.Deleted;

            return _dbContext.SaveChanges();
        }

        /// <summary>
        /// 依條件執行刪除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns>刪除筆數</returns>
        public int DeleteByFilter(Expression<Func<Tentity, bool>>? trueAndCondition = null)
        {
            if (trueAndCondition != null)
            {
                DbSet<Tentity> dbSet = _dbContext.Set<Tentity>();

                dbSet.RemoveRange(dbSet.Where(trueAndCondition));

                return _dbContext.SaveChanges();
            }

            return 0;
        }

        /// <summary>
        /// 依條件取得資料
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="trueAndCondition"></param>
        /// <returns></returns>
        public IQueryable<Tentity> Filter(Expression<Func<Tentity, bool>>? trueAndCondition = null)
        {
            DbSet<Tentity> dbSet = _dbContext.Set<Tentity>();

            if (trueAndCondition != null)
            {
                return dbSet.Where(trueAndCondition);
            }

            return dbSet;
        }

        /// <summary>
        /// 依條件取得資料
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="trueAndCondition"></param>
        /// <returns></returns>
        public Tentity? Find(Expression<Func<Tentity, bool>>? trueAndCondition = null)
        {
            DbSet<Tentity> dbSet = _dbContext.Set<Tentity>();

            if (trueAndCondition != null)
            {
                return dbSet.AsNoTracking().Where(trueAndCondition).SingleOrDefault();
            }

            return dbSet.SingleOrDefault();
        }

        /// <summary>
        /// Bulk寫入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        /// <param name="sqlBulkCopyOptions"></param>
        /// <param name="batchSize"></param>
        /// <param name="propertiesToExclude"></param>
        /// <param name="isDefaultColumnModify"></param>
        public void BulkInsert(
            IEnumerable<Tentity> entities,
            SqlBulkCopyOptions sqlBulkCopyOptions = SqlBulkCopyOptions.FireTriggers,
            int? batchSize = null,
            List<string>? propertiesToExclude = null,
            bool isDefaultColumnModify = false)
        {
            using var transaction = _dbContext.Database.BeginTransaction();

            BulkConfig bulkConfig = new BulkConfig
            {
                UseTempDB = true,
                SqlBulkCopyOptions = sqlBulkCopyOptions,
                PropertiesToExclude = propertiesToExclude
            };

            if (batchSize.HasValue) bulkConfig.BatchSize = batchSize.Value;

            _dbContext?.BulkInsert(entities.AsList(), bulkConfig);

            transaction.Commit();
        }

        /// <summary>
        /// Bulk更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        /// <param name="sqlBulkCopyOptions"></param>
        /// <param name="batchSize"></param>
        /// <param name="propertiesToExclude"></param>
        /// <param name="isDefaultColumnModify"></param>
        public void BulkUpdate(
            IEnumerable<Tentity> entities,
            SqlBulkCopyOptions sqlBulkCopyOptions = SqlBulkCopyOptions.FireTriggers,
            int? batchSize = null,
            List<string> propertiesToExclude = null,
            bool isDefaultColumnModify = false)
        {
            using var transaction = _dbContext.Database.BeginTransaction();

            BulkConfig bulkConfig = new BulkConfig
            {
                UseTempDB = true,
                SqlBulkCopyOptions = sqlBulkCopyOptions,
                PropertiesToExclude = propertiesToExclude
            };

            if (batchSize.HasValue) bulkConfig.BatchSize = batchSize.Value;

            _dbContext?.BulkUpdate(entities.AsList(), bulkConfig);

            transaction.Commit();
        }
        #endregion

        #region 共用方法
        /// <summary>
        /// 檢核Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void CheckEntityIsNull(Tentity entity) 
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
        }
        #endregion
    }
}
