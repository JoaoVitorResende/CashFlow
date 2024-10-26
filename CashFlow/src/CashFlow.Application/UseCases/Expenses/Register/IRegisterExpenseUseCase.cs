using CashFlow.Communication.Requests;

namespace CashFlow.Application.UseCases.Expenses.Register
{
    public interface IRegisterExpenseUseCase
    {
        RequestRegisterExpenseJson Execute(RequestRegisterExpenseJson request);
    }
}
