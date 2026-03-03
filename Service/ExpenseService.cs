using System;
using AutoMapper;
using Contracts;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service;

public class ExpenseService : IExpenseService
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;

    public ExpenseService(IRepositoryManager repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ExpenseDto> CreateExpenseAsync(ExpenseCreationDto expense)
    {
        var expenseEntity = _mapper.Map<Expense>(expense);
        _repository.Expense.CreateExpense(expenseEntity);
        await _repository.SaveAsync();
        var expenseDto = _mapper.Map<ExpenseDto>(expenseEntity);
        return expenseDto;
    }

    public async Task DeleteExpenseAsync(Guid expenseId, bool trackChanges)
    {
        var expense = await GetExpensesAndCheckIfItExists(expenseId, trackChanges);
        _repository.Expense.DeleteExpense(expense);
        await _repository.SaveAsync();
    }

    public async Task<IEnumerable<ExpenseDto>> GetAllExpensesAsync(bool trackChanges)
    {
        var expenses = await _repository.Expense.GetAllExpensesAsync(trackChanges);
        var ExpenseDto = _mapper.Map<IEnumerable<ExpenseDto>>(expenses);
        return ExpenseDto;
    }

    public async Task<ExpenseDto> GetExpenseAsync(Guid expenseId, bool trackChanges)
    {
        var expense = await _repository.Expense.GetExpenseAsync(expenseId, trackChanges);

        if (expense is null)
            throw new Exception($"Expense with id: {expenseId} doesn't exist in the database.");

        var expenseDto = _mapper.Map<ExpenseDto>(expense);
        return expenseDto;
    }

    public async Task UpdateExpenseAsync(Guid expenseId, ExpenseUpdationDto expenseUpdate, bool trackChanges)
    {
        var expenseEntity = await GetExpensesAndCheckIfItExists(expenseId, trackChanges);

        _mapper.Map(expenseUpdate, expenseEntity);
        await _repository.SaveAsync();
    }

    private async Task<Expense> GetExpensesAndCheckIfItExists(Guid id, bool trackChanges)
    {
        var expense = await _repository.Expense.GetExpenseAsync(id, trackChanges);
        if (expense is null)
            throw new Exception($"Expense with id: {id} doesn't exist in the database.");

        return expense;
    }
}
