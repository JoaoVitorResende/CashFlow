using CashFlow.Communication.Enuns;
using CashFlow.Communication.Requests;

namespace CashFlow.Application.UseCases.Expenses.Register;
public class RegisterExpenseUseCase
{
    public RequestRegisterExpenseJson Execute(RequestRegisterExpenseJson request)
    {
        Validate(request);
        //TO DO VALIDATIONS
        return new RequestRegisterExpenseJson();
    }
    private void Validate(RequestRegisterExpenseJson request)
    {
        var titleIsEmpty = string.IsNullOrWhiteSpace(request.Title);
        var dateResult = DateTime.Compare(request.Date, DateTime.UtcNow);
        var paymentTypeIsValid = Enum.IsDefined(typeof(PaymentType), request.PaymentType);

        if (titleIsEmpty)
            throw new ArgumentException("The title is required");
        if(request.Amount <= 0)
            throw new ArgumentException("The amount is necessary and above zero");
        if(dateResult > 0)
            throw new ArgumentException("The date must be in the present or in the past");
        if (!paymentTypeIsValid)
            throw new ArgumentException("The payment type must be one of the options");
    }
}
