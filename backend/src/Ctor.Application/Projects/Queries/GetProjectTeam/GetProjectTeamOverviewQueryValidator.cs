using FluentValidation;

namespace Ctor.Application.Projects.Queries.GetProjectTeam;

public class GetProjectTeamQueryValidator : AbstractValidator<GetProjectTeamQuery>
{
    public GetProjectTeamQueryValidator()
    {
        RuleFor(x => x.ProjectId)
            .GreaterThan(0);
    }
}