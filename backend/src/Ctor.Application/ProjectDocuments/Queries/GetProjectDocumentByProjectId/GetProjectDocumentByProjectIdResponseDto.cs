using AutoMapper;
using Ctor.Application.Common.Mapping;
using Ctor.Domain.Entities;

namespace Ctor.Application.ProjectDocuments.Queries.GetProjectDocumentByProjectId;

public class GetProjectDocumentByProjectIdResponseDto : IMapFrom<ProjectDocument>
{
    public long Id { get; set; }
    public string Link { get; set; }
    public string FileName { get; set; }
    public string BuildingName { get; set; }
    public DateTime Created { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectDocument, GetProjectDocumentByProjectIdResponseDto>()
            .ForMember(dest => dest.Link, opt => opt.MapFrom(exp => exp.Document.Link))
            .ForMember(dest => dest.FileName, opt => opt.MapFrom(exp => exp.Document.Name))
            .ForMember(dest => dest.BuildingName, opt => opt.MapFrom(exp => exp.Building.BuildingName));
    }
}