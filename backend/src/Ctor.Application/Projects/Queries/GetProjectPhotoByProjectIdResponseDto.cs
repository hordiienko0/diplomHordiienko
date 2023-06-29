using AutoMapper;
using Ctor.Application.Common.Mapping;
using Ctor.Domain.Entities;

namespace Ctor.Application.Projects.Queries;

public class GetProjectPhotoByProjectIdResponseDto : IMapFrom<ProjectPhoto>
{
    public long Id { get; set; }
    public string Link { get; set; } = string.Empty;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectPhoto, GetProjectPhotoByProjectIdResponseDto>()
            .ForMember(dest => dest.Link, opt => opt.MapFrom(exp => $"{exp.Link}"));
    }
}