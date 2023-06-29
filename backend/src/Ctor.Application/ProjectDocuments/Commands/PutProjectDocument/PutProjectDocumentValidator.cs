using FluentValidation;

namespace Ctor.Application.ProjectDocuments.Commands.PutProjectDocument;

public class PutProjectDocumentValidator : AbstractValidator<PutProjectDocumentCommand>
{
    public PutProjectDocumentValidator()
    {
        RuleFor(x => x.RequestDto.Id).GreaterThan(0);
        RuleFor(x => x.RequestDto.Name).NotEmpty();
    }
}