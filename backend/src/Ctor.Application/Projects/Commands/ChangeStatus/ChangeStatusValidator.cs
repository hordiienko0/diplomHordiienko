using FluentValidation;

namespace Ctor.Application.Projects.Commands.ChangeStatus;

public class ChangeStatusValidator : AbstractValidator<ChangeStatusCommand>
{
    public ChangeStatusValidator()
    {
        RuleFor(c => c.ProjectId)
            .GreaterThan(0);

        RuleFor(c => c.NewStatus)
            .IsInEnum();
    }
}