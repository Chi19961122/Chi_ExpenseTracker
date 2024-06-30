using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chi_ExpenseTracker_Service.Models.Category
{
    public class CategoryDto
    {
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; } = null!;
        public string? CategoryType { get; set; }
        public string? Icon { get; set; }
    }
}
