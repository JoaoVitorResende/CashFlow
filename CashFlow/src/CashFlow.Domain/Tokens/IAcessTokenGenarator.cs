using CashFlow.Domain.Entities;

namespace CashFlow.Infrastucture.Tokens;
public interface IAcessTokenGenarator
{
    string Generate(User user);
}
