using Chi_ExpenseTracker_Service.Common.Category;
using Chi_ExpenseTracker_Service.Common.Jwt;
using Chi_ExpenseTracker_Service.Common.Transaction;
using Chi_ExpenseTracker_Service.Models.Api;
using Chi_ExpenseTracker_Service.Models.Transaction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chi_ExpenseTracker_WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class TransactionController : Controller
    {
        private readonly ITransactionService _TransactionService;

        /// <summary>
        /// 服務注入
        /// </summary>
        public TransactionController(IServiceProvider serviceProvider)
        {
            _TransactionService = serviceProvider.GetService<ITransactionService>();
        }

        /// <summary>
        /// 取得全部收支明細
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public ApiResponseModel GetAllTransactions(int userId)
        {
            var result = _TransactionService.GetAllTransactions(userId);

            return new ApiResponseModel
            {
                Code = result.Code,
                Msg = result.Msg,
                Data = result.Data
            };
        }

        /// <summary>
        /// 依ID取得收支明細
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="transactionId"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResponseModel GetTransactionsById(int userId, int transactionId)
        {
            var result = _TransactionService.GetTransactionsById(userId, transactionId);

            return new ApiResponseModel
            {
                Code = result.Code,
                Msg = result.Msg,
                Data = result.Data
            };
        }

        /// <summary>
        /// 新增收支紀錄
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="categoryId"></param>
        /// <param name="amount"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResponseModel CreateTransactions(TransactionDto transactionDto)
        {
            var result = _TransactionService.CreateTransactions(transactionDto);

            return new ApiResponseModel
            {
                Code = result.Code,
                Msg = result.Msg,
                Data = result.Data
            };
        }

        /// <summary>
        /// 編輯收支紀錄
        /// </summary>
        /// <param name="transactionDto"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResponseModel EditTransactions(TransactionDto transactionDto)
        {
            var result = _TransactionService.EditTransactions(transactionDto);

            return new ApiResponseModel
            {
                Code = result.Code,
                Msg = result.Msg,
                Data = result.Data
            };
        }

        /// <summary>
        /// 刪除收支紀錄
        /// </summary>
        /// <param name="transactionId"></param>
        /// <returns></returns>
        [HttpDelete]
        public ApiResponseModel DeleteTransactions(int userId, int transactionId)
        {
            var result = _TransactionService.DeleteTransactions(userId,transactionId);

            return new ApiResponseModel
            {
                Code = result.Code,
                Msg = result.Msg,
                Data = result.Data
            };
        }

    }
}
