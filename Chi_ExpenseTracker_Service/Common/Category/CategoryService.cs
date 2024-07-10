using Chi_ExpenseTracker_Repesitory.Database.Repository;
using Chi_ExpenseTracker_Repesitory.Models;
using Chi_ExpenseTracker_Service.Common.Transaction;
using Chi_ExpenseTracker_Service.Models.Api;
using Chi_ExpenseTracker_Service.Models.Api.Enums;
using Chi_ExpenseTracker_Service.Models.Category;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chi_ExpenseTracker_Service.Common.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _CategoryRepository; 
        public CategoryService(IServiceProvider serviceProvider) 
        {
            _CategoryRepository = serviceProvider.GetService<ICategoryRepository>();
        }

        /// <summary>
        /// 取得類別
        /// </summary>
        /// <returns></returns>
        public ApiResponseModel GetCategories(int userId, string type = null)
        {
            var result = new ApiResponseModel();
            var resultData = new List<CategoryEntity>();

            if (!string.IsNullOrEmpty(type))
            {
                resultData = _CategoryRepository.Filter(x => x.UserId == userId && x.CategoryType == type).ToList();
            }
            else 
            {
                resultData = _CategoryRepository.Filter(x => x.UserId == userId).ToList();
            }

            //if (id > 0) 
            //{
            //   var resultData = _CategoryRepository.Filter(x => x.CategoryId == id).FirstOrDefault();
            //   result.Data = resultData;
            //}
            //else 
            //{
            //    var resultData = _CategoryRepository.Filter().ToList();
            //    result.Data = resultData;
            //}

            result.Data = resultData;
            result.ApiResult(ApiCodeEnum.Success);
            return result;
        }

        /// <summary>
        /// 新增類別
        /// </summary>
        /// <param name="categoryEntity"></param>
        /// <returns></returns>
        public ApiResponseModel CreateCategories(CategoryDto categoryDto)
        {
            var result = new ApiResponseModel();

            var categoryData = new CategoryEntity() 
            {
                UserId = categoryDto.UserId,
                Title = categoryDto.Title,
                CategoryType = categoryDto.CategoryType,
                Icon = categoryDto.Icon,
            };

            var resultData = _CategoryRepository.Add(categoryData);

            result.ApiResult(ApiCodeEnum.Success);
            result.Data = resultData;
            return result;
        }

        /// <summary>
        /// 更新類別
        /// </summary>
        /// <param name="categoryEntity"></param>
        /// <returns></returns>
        public ApiResponseModel EditCategories(CategoryDto categoryDto)
        {
            var result = new ApiResponseModel();

            var categoryData = new CategoryEntity()
            {
                UserId = categoryDto.UserId,
                CategoryId = categoryDto.CategoryId,
                Title = categoryDto.Title,
                CategoryType = categoryDto.CategoryType,
                Icon = categoryDto.Icon,
            };

            var resultData = _CategoryRepository.Update(categoryData);

            result.ApiResult(ApiCodeEnum.Success);
            result.Data = resultData;
            return result;
        }

        /// <summary>
        /// 依ID刪除類別
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public ApiResponseModel DeleteCategoryById(int userId, int categoryId)
        {
            var result = new ApiResponseModel();

            var resultData = _CategoryRepository.DeleteByFilter(x => x.UserId == userId && x.CategoryId == categoryId);

            result.ApiResult(ApiCodeEnum.Success);
            result.Data = resultData;
            return result;
        }
    }
}
