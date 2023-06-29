using Ctor.Application.Common.Mapping;
using Ctor.Domain.Entities;

namespace Ctor.Application.ProjectDocuments.Commands.PostProjectDocument;

public class PostProjectDocumentResponseDto : IMapFrom<ProjectDocument>
{
    public long Id { get; set; }
    public long BuildingId { get; set; }
}