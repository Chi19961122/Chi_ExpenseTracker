using Chi_ExpenseTracker_Repesitory.Models;
using Chi_ExpenseTracker_Service.Models.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chi_ExpenseTracker_Service.Common.Category
{
    public interface ICategoryService
    {
        ApiResponseModel GetCategories(int id = 0);

        ApiResponseModel CreateCategories(CategoryEntity categoryEntity);

        ApiResponseModel EditCategories(CategoryEntity categoryEntity);

        ApiResponseModel DeleteCategoryById(int categoryId);
    }
}
