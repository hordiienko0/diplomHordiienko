using AutoMapper;
using Ctor.Application.Common.Mapping;
using Ctor.Domain.Entities;

namespace Ctor.Application.ProjectDocuments.Commands.DeleteProjectDocument;

public class DeleteProjectDocumentPathDto : IMapFrom<ProjectDocument>
{
    public long Id { get; set; }
    public long ProjectId { get; set; }
    public long DocumentId { get; set; }
    public string Path { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectDocument, DeleteProjectDocumentPathDto>()
            .ForMember(dest => dest.Path, opt => opt.MapFrom(exp => exp.Document.Path))
            .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(exp => exp.Building.ProjectId));
    }
}