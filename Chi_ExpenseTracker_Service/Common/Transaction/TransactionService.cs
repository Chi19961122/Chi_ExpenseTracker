using Chi_ExpenseTracker_Repesitory.Database.Repository;
using Chi_ExpenseTracker_Repesitory.Models;
using Chi_ExpenseTracker_Service.Models.Api;
using Chi_ExpenseTracker_Service.Models.Api.Enums;
using Chi_ExpenseTracker_Service.Models.Transaction;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
        private readonly ICategoryRepository _CategoryRepository;
        public TransactionService(IServiceProvider serviceProvider)
        {
            _TransactionRepo = serviceProvider.GetService<ITransactionRepository>();
            _CategoryRepository = serviceProvider.GetService<ICategoryRepository>();
        }

        /// <summary>
        /// 取得全部收支明細
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ApiResponseModel GetAllTransactions(int userId)
        {
            var result = new ApiResponseModel();
            var resultData = new List<GetAllTransResDto>();

            var transData = _TransactionRepo.Filter(x => x.UserId == userId).ToList();

            var categoryData = _CategoryRepository.Filter(x => x.UserId == userId).ToList();

            resultData = transData.Join(categoryData,
                transaction => transaction.CategoryId,
                category => category.CategoryId,
                (transaction, category) => new GetAllTransResDto
                {
                    TransactionId = transaction.TransactionId,
                    UserId = transaction.UserId,
                    CategoryId = transaction.CategoryId,
                    CategoryName = category.Title,
                    CategoryType = category.CategoryType,
                    Amount = transaction.Amount,
                    CreateDate = transaction.CreateDate,
                    Description = transaction.Description,
                }).ToList();

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
                Description = transactionDto.Description,
                CreateDate = transactionDto.CreateDate,
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
        public ApiResponseModel DeleteTransactions(int userId, int transactionId)
        {
            var result = new ApiResponseModel();

            var resultData = _TransactionRepo.DeleteByFilter(x => x.UserId == userId && x.TransactionId == transactionId);

            result.ApiResult(ApiCodeEnum.Success);
            result.Data = resultData;
            return result;
        }

    }

    public class GetAllTransResDto
    {
        public int TransactionId { get; set; }

        public int UserId { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string CategoryType { get; set; }

        public decimal Amount { get; set; }

        public DateTime CreateDate { get; set; }

        public string? Description { get; set; }

    }
}
