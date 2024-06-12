using Chi_ExpenseTracker_Repesitory.Database.Repository;
using Chi_ExpenseTracker_Repesitory.Models;
using Chi_ExpenseTracker_Service.Models.Api;
using Chi_ExpenseTracker_Service.Models.Api.Enums;
using Chi_ExpenseTracker_Service.Models.Transaction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Chi_ExpenseTracker_Service.Common.Transaction
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _TransactionRepo;
        public TransactionService(ITransactionRepository transactionRepo)
        {
            _TransactionRepo = transactionRepo;
        }

        /// <summary>
        /// 取得全部收支明細
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ApiResponseModel GetAllTransactions(int userId)
        {
            var result = new ApiResponseModel();

            var resultData = _TransactionRepo.Filter(x => x.UserId == userId);

            result.ApiResult(ApiCodeEnum.Success);
            result.Data = resultData;
            return result;
        }

        /// <summary>
        /// 依ID取得收支明細
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ApiResponseModel GetTransactionsById(int userId, int transactionId)
        {
            var result = new ApiResponseModel();

            var resultData = _TransactionRepo.Filter(x => x.UserId == userId && x.TransactionId == transactionId);

            result.ApiResult(ApiCodeEnum.Success);
            result.Data = resultData;
            return result;
        }

        /// <summary>
        /// 新增收支紀錄
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="categoryId"></param>
        /// <param name="amount"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public ApiResponseModel CreateTransactions(TransactionDto transactionDto)
        {
            var result = new ApiResponseModel();

            var transactionData = new TransactionEntity
            {
                UserId = transactionDto.UserId,
                CategoryId = transactionDto.CategoryId,
                Amount = transactionDto.Amount,
                Description = transactionDto.Description
            };

            var resultData = _TransactionRepo.Add(transactionData);

            result.ApiResult(ApiCodeEnum.Success);
            result.Data = resultData;
            return result;
        }

        /// <summary>
        /// 編輯收支紀錄
        /// </summary>
        /// <param name="transactionDto"></param>
        /// <returns></returns>
        public ApiResponseModel EditTransactions(TransactionDto transactionDto)
        {
            var result = new ApiResponseModel();

            var transactionData = new TransactionEntity
            {
                TransactionId = transactionDto.TransactionId,
                UserId = transactionDto.UserId,
                CategoryId = transactionDto.CategoryId,
                Amount = transactionDto.Amount,
                Description = transactionDto.Description
            };

            var resultData = _TransactionRepo.Update(transactionData);

            result.ApiResult(ApiCodeEnum.Success);
            result.Data = resultData;
            return result;
        }

        /// <summary>
        /// 刪除收支紀錄
        /// </summary>
        /// <param name="transactionId"></param>
        /// <returns></returns>
        public ApiResponseModel DeleteTransactions(int transactionId)
        {
            var result = new ApiResponseModel();

            var resultData = _TransactionRepo.DeleteByFilter(x => x.TransactionId == transactionId);

            result.ApiResult(ApiCodeEnum.Success);
            result.Data = resultData;
            return result;
        }

    }
}
