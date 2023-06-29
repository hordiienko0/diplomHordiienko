using FluentValidation;

namespace Ctor.Application.ProjectDocuments.Queries.GetProjectDocumentByProjectId;

public class GetProjectDocumentByProjectIdQueryValidator : AbstractValidator<GetProjectDocumentsByProjectIdQuery>
{
    public GetProjectDocumentByProjectIdQueryValidator()
    {
        RuleFor(x => x.ProjectId).GreaterThan(0);
    }
}