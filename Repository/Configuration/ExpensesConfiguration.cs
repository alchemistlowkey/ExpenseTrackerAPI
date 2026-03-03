using System;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration;

public class ExpensesConfiguration : IEntityTypeConfiguration<Expense>
{
    public void Configure(EntityTypeBuilder<Expense> builder)
    {
        builder.Property(e => e.Amount).HasPrecision(18, 2);

        builder.Property(e => e.Category).HasConversion<string>();

        builder.HasData(
            new Expense { Id = new Guid("b0788d2f-8003-43c1-92a4-edc76a7c5dda"), Description = "Tapioca", Amount = 100, Category = ExpenseCategory.Groceries },
            new Expense { Id = new Guid("b0788d2f-8003-43c1-92a4-edc76a7c5ddb"), Description = "Freezer", Amount = 1500, Category = ExpenseCategory.Electronics },
            new Expense { Id = new Guid("b0788d2f-8003-43c1-92a4-edc76a7c5ddc"), Description = "Malaria Treatment", Amount = 400, Category = ExpenseCategory.Health }
        );
    }
}
