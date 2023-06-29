using Ctor.Application.Common.Mapping;

namespace Ctor.Application.ProjectDocuments.Commands.DeleteProjectDocument;

public class DeleteProjectDocumentResponseDto : IMapFrom<DeleteProjectDocumentPathDto>
{
    public long Id { get; set; }
    public long ProjectId { get; set; }
}