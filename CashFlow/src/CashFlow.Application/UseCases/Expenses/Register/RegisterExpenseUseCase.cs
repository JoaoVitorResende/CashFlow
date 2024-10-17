using CashFlow.Communication.Requests;

namespace CashFlow.Application.UseCases.Expenses.Register;
public class RegisterExpenseUseCase
{
    public RequestRegisterExpenseJson Execute(RequestRegisterExpenseJson request)
    {
        Validate(request);
        return new RequestRegisterExpenseJson();
    }
    private void Validate(RequestRegisterExpenseJson request)
    {
        var validator = new RegisterExpenseValidator();
        var result = validator.Validate(request);
        if (!result.IsValid)
        {
            var errosMessages = result.Errors.Select(f => f.ErrorMessage).ToList();
        }
    }
}
