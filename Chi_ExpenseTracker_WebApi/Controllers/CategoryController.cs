using Chi_ExpenseTracker_Repesitory.Models;
using Chi_ExpenseTracker_Service.Common.Category;
using Chi_ExpenseTracker_Service.Models.Api;
using Microsoft.AspNetCore.Mvc;

namespace Chi_ExpenseTracker_WebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _CategoryService;

        /// <summary>
        /// 服務注入
        /// </summary>
        public CategoryController(ICategoryService categoryService) 
        {
            _CategoryService = categoryService;
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
        /// <param name="categoryEntity"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResponseModel CreateCategories(CategoryEntity categoryEntity)
        {
            var result = _CategoryService.CreateCategories(categoryEntity);

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
        /// <param name="categoryEntity"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResponseModel EditCategories(CategoryEntity categoryEntity)
        {
            var result = _CategoryService.EditCategories(categoryEntity);

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
