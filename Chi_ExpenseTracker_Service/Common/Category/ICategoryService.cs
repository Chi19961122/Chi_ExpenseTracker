using Chi_ExpenseTracker_Repesitory.Models;
using Chi_ExpenseTracker_Service.Models.Api;
using Chi_ExpenseTracker_Service.Models.Category;
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

        ApiResponseModel CreateCategories(CategoryDto categoryEntity);

        ApiResponseModel EditCategories(CategoryDto categoryDto);

        ApiResponseModel DeleteCategoryById(int userId, int categoryId);
    }
}
