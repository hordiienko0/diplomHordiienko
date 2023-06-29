using AutoMapper;
using Ctor.Application.Common.Mapping;
using Ctor.Domain.Entities;

namespace Ctor.Application.ProjectDocuments.Commands.PutProjectDocument;

public class PutProjectDocumentRequestDto : IMapFrom<ProjectDocument>
{
    public long Id { get; set; }
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectDocument, PutProjectDocumentRequestDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(exp => exp.Document.Name)).ReverseMap();
    }
}