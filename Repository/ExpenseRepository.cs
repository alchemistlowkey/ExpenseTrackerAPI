using System;
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class ExpenseRepository : RepositoryBase<Expense>, IExpenseRepository
{
    public ExpenseRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {

    }

    public void CreateExpense(Expense expense) => Create(expense);

    public void DeleteExpense(Expense expense) => Delete(expense);

    public async Task<IEnumerable<Expense>> GetAllExpensesAsync(bool trackChanges) =>
        await FindAll(trackChanges)
        .ToListAsync();

    public async Task<Expense> GetExpenseAsync(Guid id, bool trackChanges) =>
        await FindByCondition(e => e.Id.Equals(id), trackChanges)
        .SingleOrDefaultAsync();

    public void UpdateExpense(Expense expense) => Update(expense);
}
