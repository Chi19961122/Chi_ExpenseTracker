using Chi_ExpenseTracker_Repesitory.Database.Repository;
using Chi_ExpenseTracker_Repesitory.Models;
using Chi_ExpenseTracker_Service.Models.Api;
using Chi_ExpenseTracker_Service.Models.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chi_ExpenseTracker_Service.Common.Transaction
{
    public interface ITransactionService
    {
        ApiResponseModel CreateTransactions(TransactionDto transactionDto);

        ApiResponseModel EditTransactions(TransactionDto transactionDto);

        ApiResponseModel DeleteTransactions(int userId, int transactionId);

        ApiResponseModel GetAllTransactions(int userId);

        ApiResponseModel GetTransactionsById(int userId, int transactionId);
    }
}
