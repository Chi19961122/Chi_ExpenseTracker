﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Chi_ExpenseTracker_Repesitory.Models;

public partial class TransactionEntity
{
    [Key]
    public int TransactionId { get; set; }

    public int UserId { get; set; }

    public int CategoryId { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Amount { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreateDate { get; set; }

    [StringLength(250)]
    public string? Description { get; set; }

    [ForeignKey("CategoryId")]
    [InverseProperty("Transactions")]
    public virtual CategoryEntity Category { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("Transactions")]
    public virtual UserEntity User { get; set; } = null!;
}
