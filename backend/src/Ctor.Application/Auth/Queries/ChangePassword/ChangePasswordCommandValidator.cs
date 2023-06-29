using FluentValidation;

namespace Ctor.Application.Auth.Queries.ChangePassword;

public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator()
    {
        RuleFor(x => x.CurrentPassword)
            .NotEmpty()
            .MinimumLength(5);

        RuleFor(x => x.NewPassword)
            .NotEmpty()
            .MinimumLength(5);
    }
}