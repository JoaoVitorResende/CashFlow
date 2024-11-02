using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCases.Expenses.GetFromId;
public interface IGetExpenseIdUseCase
{
    Task<ResponseExpensesJson> Execute(long id);
}
