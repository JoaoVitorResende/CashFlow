﻿using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastucture.DataAccess.Repositories;
internal class ExpensesRepository : IExpensesReadOnlyRepository, IExpensesWriteOnlyRepository
{
    private readonly CashFlowDbContex _dbContext;
    public ExpensesRepository(CashFlowDbContex dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Add(Expense expense)
    {
        await _dbContext.Expenses.AddAsync(expense);
    }
    public async Task<List<Expense>> GetAll()
    {
        return await _dbContext.Expenses.AsNoTracking().ToListAsync();
    }
    public async Task<Expense?> GetById(long id)
    {
        return await _dbContext.Expenses.AsNoTracking().FirstOrDefaultAsync(expense => expense.Id == id);
    }
    public async Task<bool> Delete(long id)
    {
        var result = await _dbContext.Expenses.FirstOrDefaultAsync(expense => expense.Id == id);
        if (result is null)
            return false;
        _dbContext.Expenses.Remove(result);
        return true;
    }
}
