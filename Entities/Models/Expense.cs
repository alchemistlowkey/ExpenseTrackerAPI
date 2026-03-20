using System;

namespace Entities.Models;

public class Expense
{
    public Guid Id { get; set; }
    public string Date { get; set; } = DateTime.UtcNow.ToString("yyyy-MM-dd");
    public required string Description { get; set; }
    public decimal Amount { get; set; }
    public ExpenseCategory Category { get; set; } = ExpenseCategory.Others;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow.Date;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow.Date;
}
