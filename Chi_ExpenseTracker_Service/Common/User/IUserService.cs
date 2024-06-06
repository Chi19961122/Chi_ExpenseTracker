using Chi_ExpenseTracker_Repesitory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chi_ExpenseTracker_Service.Common.User
{
    public interface IUserService
    {
        UserEntity GetUserByAccount(string account);
    }
}
