using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Chi_ExpenseTracker_Repesitory.Models;

public partial class _ExpenseDbContext : DbContext
{
    public _ExpenseDbContext(DbContextOptions<_ExpenseDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CategoryEntity> Categories { get; set; }

    public virtual DbSet<TransactionEntity> Transactions { get; set; }

    public virtual DbSet<UserEntity> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CategoryEntity>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Categori__19093A0B703F2409");

            entity.Property(e => e.CategoryType).HasDefaultValue("Expense");
            entity.Property(e => e.Icon).HasDefaultValue("");

            entity.HasOne(d => d.User).WithMany(p => p.Categories)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Categories_Users");
        });

        modelBuilder.Entity<TransactionEntity>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK__Transact__55433A6B3F3289E8");

            entity.HasOne(d => d.Category).WithMany(p => p.Transactions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Transacti__Categ__19DFD96B");

            entity.HasOne(d => d.User).WithMany(p => p.Transactions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Transacti__UserI__18EBB532");
        });

        modelBuilder.Entity<UserEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3213E83FD8D0B2F3");

            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
