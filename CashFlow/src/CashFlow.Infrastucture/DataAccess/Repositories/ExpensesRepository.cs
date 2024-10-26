using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;

namespace CashFlow.Infrastucture.DataAccess.Repositories;
internal class ExpensesRepository : IExpensesRepository
{
    public void Add(Expense expense)
    {
        var dbContext = new CashFlowDbContex();
        dbContext.Add(expense);
        dbContext.SaveChanges();
    }
}
