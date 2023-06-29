using FluentValidation;

namespace Ctor.Application.Projects.Commands.SetProjectTeam;

public class SetProjectTeamCommandValidator : AbstractValidator<SetProjectTeamCommand>
{
    public SetProjectTeamCommandValidator()
    {
        RuleFor(c => c.ProjectId)
            .GreaterThan(0);
    }
}