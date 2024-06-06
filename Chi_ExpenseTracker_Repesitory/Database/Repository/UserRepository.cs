using Chi_ExpenseTracker_Repesitory.Models;
using System.Linq.Expressions;

namespace Chi_ExpenseTracker_Repesitory.Database.Repository
{
    public class UserRepository : DbBase<UserEntity, _ExpenseDbContext>, IUserRepository
    {
        public UserRepository(_ExpenseDbContext dbContext) : base(dbContext)
        {
        }

        public int UpdateRefreshToken(UserEntity user)
        {
            const string sql = @" UPDATE [Users]
                                     SET RefreshToken = @refreshToken
                                   WHERE Email = @email ";

            return ExecuteSqlCommand(sql, new 
            {
                email = user.Email, 
                refreshToken = user.RefreshToken 
            });
        }
    }
}
