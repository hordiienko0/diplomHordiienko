using FluentValidation;

namespace Ctor.Application.Auth.Queries.KeepDefaultPassword;

public class KeepDefaultPasswordCommandValidator : AbstractValidator<KeepDefaultPasswordCommand>
{
    public KeepDefaultPasswordCommandValidator()
    {
    }
}