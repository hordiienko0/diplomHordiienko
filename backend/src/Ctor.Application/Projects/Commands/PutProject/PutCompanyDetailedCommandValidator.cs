using FluentValidation;

namespace Ctor.Application.Companies.Commands;

public class PutProjectDetailedCommandValidator : AbstractValidator<PutProjectDetailedCommand>
{
    public PutProjectDetailedCommandValidator()
    {
        RuleFor(c => c.Project.Id).GreaterThan(0);
        RuleFor(c => c.Project.Address).NotEmpty();
        RuleFor(c => c.Project.StartTime).NotEmpty();
        RuleFor(c => c.Project.EndTime).GreaterThanOrEqualTo(c => c.Project.StartTime).NotEmpty();
    }
}