using Chi_ExpenseTracker_Repesitory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chi_ExpenseTracker_Repesitory.Database.Repository
{
    public class TransactionRepository : DbBase<TransactionEntity, _ExpenseDbContext>, ITransactionRepository 
    {
        public TransactionRepository(_ExpenseDbContext dbContext) : base(dbContext)
        {
        }
    }
}
