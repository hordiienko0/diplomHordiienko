using FluentValidation;

namespace Ctor.Application.Auth.Queries.ForgotPassword;

public class ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommand>
{
    public ForgotPasswordCommandValidator()
    {
        RuleFor(v => v.Email)
            .EmailAddress();
    }
}