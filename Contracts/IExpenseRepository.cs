using System;
using Entities.Models;

namespace Contracts;

public interface IExpenseRepository
{
    Task<IEnumerable<Expense>> GetAllExpensesAsync(bool trackChanges);
    Task<Expense> GetExpenseAsync(Guid id, bool trackChanges);
    void CreateExpense(Expense expense);
    void UpdateExpense(Expense expense);
    void DeleteExpense(Expense expense);
}
