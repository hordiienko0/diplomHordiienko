using AutoMapper;
using Ctor.Application.Common.Mapping;
using Ctor.Application.Projects.Queries;
using Ctor.Domain.Entities;
using Ctor.Domain.Entities.Enums;

namespace Ctor.Application.Projects.Queries.GetProjectsByCompanyId;

public class ProjectBriefDto : IMapFrom<Project>
{
    public long Id { get; set; }
    public string ProjectName { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Project, ProjectBriefDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

    }
}
