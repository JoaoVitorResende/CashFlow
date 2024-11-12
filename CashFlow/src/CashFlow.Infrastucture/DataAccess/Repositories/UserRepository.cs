using CashFlow.Domain.Repositories.Users;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastucture.DataAccess.Repositories;
internal class UserRepository : IUsersReadOnlyRepository
{
    private readonly CashFlowDbContex _dbContext;
    public UserRepository(CashFlowDbContex dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<bool> ExistActiveUserWithEmail(string email)
    {
        return await _dbContext.Users.AnyAsync(user => user.Email.Equals(email));
    }
}
