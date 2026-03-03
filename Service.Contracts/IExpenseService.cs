using System;
using Entities.Models;
using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface IExpenseService
{
    Task<IEnumerable<ExpenseDto>> GetAllExpensesAsync(bool trackChanges);
    Task<ExpenseDto> GetExpenseAsync(Guid expenseId, bool trackChanges);
    Task<ExpenseDto> CreateExpenseAsync(ExpenseCreationDto expense);
    Task DeleteExpenseAsync(Guid expenseId, bool trackChanges);
    Task UpdateExpenseAsync(Guid expenseId, ExpenseUpdationDto expense, bool trackChanges);
}
