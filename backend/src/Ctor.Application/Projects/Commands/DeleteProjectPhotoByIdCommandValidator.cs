using FluentValidation;

namespace Ctor.Application.Projects.Commands;

public class DeleteProjectPhotoByIdCommandValidator : AbstractValidator<DeleteProjectPhotoByIdCommand>
{
    public DeleteProjectPhotoByIdCommandValidator()
    {
        RuleFor(e => e.ProjectPhotoId).GreaterThan(0);
        RuleFor(e => e.ProjectId).GreaterThan(0);
    }
}