using System;
using AutoMapper;
using Contracts;
using Entities.ConfigurationModels;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Service.Contracts;

namespace Service;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IExpenseService> _expenseService;
    private readonly Lazy<IAuthenticationService> _authenticationService;

    public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper, UserManager<User> userManager, IOptions<JwtConfiguration> configuration, IConfiguration appConfiguration)
    {
        _expenseService = new Lazy<IExpenseService>(() => new ExpenseService(repositoryManager, mapper));
        _authenticationService = new Lazy<IAuthenticationService>(() => new
        AuthenticationService(mapper, userManager, configuration, appConfiguration));
    }

    public IExpenseService ExpenseService => _expenseService.Value;

    public IAuthenticationService AuthenticationService => _authenticationService.Value;
}
