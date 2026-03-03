using System;

namespace Service.Contracts;

public interface IServiceManager
{
    IExpenseService ExpenseService { get; }
    IAuthenticationService AuthenticationService { get; }
}
