using FluentValidation;

namespace Ctor.Application.ProjectDocuments.Commands.PostProjectDocument;

public class PostProjectDocumentCommandValidator : AbstractValidator<PostProjectDocumentCommand>
{
    public PostProjectDocumentCommandValidator()
    {
        RuleFor(x => x.BuildingId).GreaterThan(0);

        When(x => x.Files.Count == 0, () =>
        {
            RuleFor(x => x.Urls).NotEmpty();
        });

        When(x => x.Urls.Length == 0, () =>
        {
            RuleFor(x => x.Files).NotEmpty();
        });
    }
}