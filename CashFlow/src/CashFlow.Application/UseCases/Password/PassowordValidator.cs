using FluentValidation;
using FluentValidation.Validators;

namespace CashFlow.Application.UseCases.Password;
public class PassowordValidator<T> : PropertyValidator<T, string>
{
    public override string Name => "PasswordValidator";

    public override bool IsValid(ValidationContext<T> context, string password)
    {
        throw new NotImplementedException();
    }
}
