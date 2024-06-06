using System;
using System.Collections.Generic;

namespace Chi_ExpenseTracker_Repesitory.Models;

public partial class CategoryEntity
{
    public int CategoryId { get; set; }

    public string Title { get; set; } = null!;

    public string? Icon { get; set; }

    public string? CategoryType { get; set; }
}
