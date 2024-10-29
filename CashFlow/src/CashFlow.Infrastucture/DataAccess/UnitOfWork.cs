using CashFlow.Domain.Repositories;

namespace CashFlow.Infrastucture.DataAccess;
internal class UnitOfWork : IUnitOfWork
{
    private readonly CashFlowDbContex _dbContext;
    public UnitOfWork(CashFlowDbContex dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Commit()
    {
        await _dbContext.SaveChangesAsync();
    }
}
