using System;
using System.Collections.Generic;

namespace Chi_ExpenseTracker_Repesitory.Models;

public partial class UserEntity
{
    public int Id { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime CreateDate { get; set; }

    public string? RefreshToken { get; set; }

    public string? Role { get; set; }
}
