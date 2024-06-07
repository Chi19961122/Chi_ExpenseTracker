using Chi_ExpenseTracker_Repesitory.Database.Repository;
using Chi_ExpenseTracker_Repesitory.Models;
using Chi_ExpenseTracker_Service.Models.Api;
using Chi_ExpenseTracker_Service.Models.Api.Enums;
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
        public CategoryService(ICategoryRepository categoryRepository) 
        {
            _CategoryRepository = categoryRepository;
        }

        /// <summary>
        /// 取得類別
        /// </summary>
        /// <returns></returns>
        public ApiResponseModel GetCategories(int id = 0)
        {
            var result = new ApiResponseModel();

            if (id > 0) 
            {
               var resultData = _CategoryRepository.Filter(x => x.CategoryId == id).FirstOrDefault();
               result.Data = resultData;
            }
            else 
            {
                var resultData = _CategoryRepository.Filter().ToList();
                result.Data = resultData;
            }

            result.ApiResult(ApiCodeEnum.Success);
            return result;
        }

        /// <summary>
        /// 新增類別
        /// </summary>
        /// <param name="categoryEntity"></param>
        /// <returns></returns>
        public ApiResponseModel CreateCategories(CategoryEntity categoryEntity)
        {
            var result = new ApiResponseModel();

            var resultData = _CategoryRepository.Add(categoryEntity);

            result.ApiResult(ApiCodeEnum.Success);
            result.Data = resultData;
            return result;
        }
        /// <summary>
        /// 更新類別
        /// </summary>
        /// <param name="categoryEntity"></param>
        /// <returns></returns>
        public ApiResponseModel EditCategories(CategoryEntity categoryEntity)
        {
            var result = new ApiResponseModel();

            var resultData = _CategoryRepository.Update(categoryEntity);

            result.ApiResult(ApiCodeEnum.Success);
            result.Data = resultData;
            return result;
        }

        /// <summary>
        /// 依ID刪除類別
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public ApiResponseModel DeleteCategoryById(int categoryId)
        {
            var result = new ApiResponseModel();

            var resultData = _CategoryRepository.DeleteByFilter(x => x.CategoryId == categoryId);

            result.ApiResult(ApiCodeEnum.Success);
            result.Data = resultData;
            return result;
        }
    }
}
