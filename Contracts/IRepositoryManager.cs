using System;

namespace Contracts;

public interface IRepositoryManager
{
    IExpenseRepository Expense { get; }
    Task SaveAsync();
}
