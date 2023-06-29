using FluentValidation;

namespace Ctor.Application.Projects.Queries;

public class GetProjectPhotosByProjectIdQueryValidator : AbstractValidator<GetProjectPhotosByProjectIdQuery>
{
    public GetProjectPhotosByProjectIdQueryValidator()
    {
        RuleFor(e => e.ProjectId).GreaterThan(0);
    }
}