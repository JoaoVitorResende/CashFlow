﻿using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;

namespace CashFlow.Infrastucture.DataAccess.Repositories;
internal class ExpensesRepository : IExpensesRepository
{
    private readonly CashFlowDbContex _dbContext;
    public ExpensesRepository(CashFlowDbContex dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Add(Expense expense)
    {
        await _dbContext.AddAsync(expense);
    }
}
