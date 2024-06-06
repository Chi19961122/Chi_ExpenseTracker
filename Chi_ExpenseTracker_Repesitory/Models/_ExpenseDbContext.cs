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

    public virtual DbSet<UserEntity> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CategoryEntity>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Categori__19093A0B703F2409");

            entity.Property(e => e.CategoryType)
                .HasMaxLength(10)
                .HasDefaultValue("Expense");
            entity.Property(e => e.Icon)
                .HasMaxLength(5)
                .HasDefaultValue("");
            entity.Property(e => e.Title).HasMaxLength(50);
        });

        modelBuilder.Entity<UserEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3213E83FD8D0B2F3");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.RefreshToken).HasMaxLength(1024);
            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
