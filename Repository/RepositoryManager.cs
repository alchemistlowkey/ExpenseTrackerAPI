using System;
using Contracts;

namespace Repository;

public class RepositoryManager : IRepositoryManager
{
    private readonly RepositoryContext _repositoryContext;
    private readonly Lazy<IExpenseRepository> _expenseRepository;

    public RepositoryManager(RepositoryContext repositoryContext)
    {
        _repositoryContext = repositoryContext;
        _expenseRepository = new Lazy<IExpenseRepository>(() => new ExpenseRepository(_repositoryContext));
    }

    public IExpenseRepository Expense => _expenseRepository.Value;

    public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
}
