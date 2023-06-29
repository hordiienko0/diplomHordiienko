using FluentValidation;

namespace Ctor.Application.Auth.Queries.Logout;

public class LogoutCommandValidator : AbstractValidator<LogoutCommand>
{
    public LogoutCommandValidator()
    {
    }
}