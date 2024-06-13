using Chi_ExpenseTracker_Repesitory.Database.Repository;
using Chi_ExpenseTracker_Repesitory.Models;
using Chi_ExpenseTracker_Service.Common.User;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chi_ExpenseTracker_Service.Common.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IServiceProvider serviceProvider)
        {
            _userRepository = serviceProvider.GetService<IUserRepository>();
        }

        public UserEntity GetUserByAccount(string account)
        {
            if (string.IsNullOrEmpty(account))
            {
                throw new ArgumentException("Account cannot be null or empty", nameof(account));
            }

            return _userRepository.Find(user => user.Email == account);
        }


    }

}
