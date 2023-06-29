using FluentValidation;

namespace Ctor.Application.Auth.Queries.ChangeDefaultPassword;

public class ChangeDefaultPasswordCommandValidator : AbstractValidator<ChangeDefaultPasswordCommand>
{
    public ChangeDefaultPasswordCommandValidator()
    {
        RuleFor(x => x.NewPassword)
            .NotEmpty()
            .MinimumLength(5);
    }
}