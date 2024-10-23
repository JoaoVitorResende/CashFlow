using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Requests;

namespace Validators.Test.Expenses.Register;
public class RegisterExpenseValidatorTests
{
    [Fact]
    public void Success()
    {
        //arrange
        var validator = new RegisterExpenseValidator();
        var request = new RequestRegisterExpenseJson()
        {
            Amount = 100,
            Date = DateTime.Now,
            Description = "Description",
            Title = "Apple",
            PaymentType = CashFlow.Communication.Enuns.PaymentType.cash
        };
        //act
        var result = validator.Validate(request);
        //assert
        Assert.True(result.IsValid);
    }
}
