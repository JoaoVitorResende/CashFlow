using CashFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastucture.DataAccess;
internal class CashFlowDbContex : DbContext
{
    public CashFlowDbContex(DbContextOptions options) : base(options) { }
    public DbSet<Expense> Expenses{get;set;}
    public DbSet<User> Users{get;set;}
}
