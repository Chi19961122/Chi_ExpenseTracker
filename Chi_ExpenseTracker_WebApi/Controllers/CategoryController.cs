using Chi_ExpenseTracker_Repesitory.Models;
using Chi_ExpenseTracker_Service.Common.Category;
using Chi_ExpenseTracker_Service.Models.Api;
using Chi_ExpenseTracker_Service.Models.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chi_ExpenseTracker_WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _CategoryService;

        /// <summary>
        /// 服務注入
        /// </summary>
        public CategoryController(IServiceProvider serviceProvider) 
        {
            _CategoryService = serviceProvider.GetService<ICategoryService>();
        }

        /// <summary>
        /// 取得類別
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResponseModel GetCategories(int id = 0) 
        {
            var result = _CategoryService.GetCategories(id);

            return new ApiResponseModel
            {
                Code = result.Code,
                Msg = result.Msg,
                Data = result.Data
            };
        }

        /// <summary>
        /// 新增類別
        /// </summary>
        /// <param name="categoryDto"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResponseModel CreateCategories(CategoryDto categoryDto)
        {
            var result = _CategoryService.CreateCategories(categoryDto);

            return new ApiResponseModel
            {
                Code = result.Code,
                Msg = result.Msg,
                Data = result.Data
            };
        }

        /// <summary>
        /// 編輯類別
        /// </summary>
        /// <param name="categoryDto"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResponseModel EditCategories(CategoryDto categoryDto)
        {
            var result = _CategoryService.EditCategories(categoryDto);

            return new ApiResponseModel
            {
                Code = result.Code,
                Msg = result.Msg,
                Data = result.Data
            };
        }

        /// <summary>
        ///  依ID刪除類別
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [HttpDelete]
        public ApiResponseModel DeleteCategoryById(int categoryId)
        {
            var result = _CategoryService.DeleteCategoryById(categoryId);

            return new ApiResponseModel
            {
                Code = result.Code,
                Msg = result.Msg,
                Data = result.Data
            };
        }
    }
}
