using FluentValidation;

namespace Ctor.Application.Projects.Commands;

public class PutProjectPhotoCommandValidator : AbstractValidator<PutProjectPhotosCommand>
{
    public PutProjectPhotoCommandValidator()
    {
        RuleFor(e => e.ProjectId).GreaterThan(0);
    }
}