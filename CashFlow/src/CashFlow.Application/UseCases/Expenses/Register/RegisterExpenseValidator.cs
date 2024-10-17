using CashFlow.Communication.Requests;
using FluentValidation;

namespace CashFlow.Application.UseCases.Expenses.Register;
public class RegisterExpenseValidator: AbstractValidator<RequestRegisterExpenseJson>
{
    public RegisterExpenseValidator()
    {
        RuleFor(expense => expense.Title).NotEmpty().WithMessage("the title must not be empty");
        RuleFor(expense => expense.Amount).GreaterThan(0).WithMessage("the amount must be greater then zero");
        RuleFor(expense => expense.Date).LessThanOrEqualTo(DateTime.UtcNow).WithMessage("The date must be in the present or in the past");
        RuleFor(expense => expense.PaymentType).IsInEnum().WithMessage("The payment type must be one of the options");
    }
}
