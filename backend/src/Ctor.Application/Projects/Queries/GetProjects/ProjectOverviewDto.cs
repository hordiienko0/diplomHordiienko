using AutoMapper;
using Ctor.Application.Common.Mapping;
using Ctor.Application.Projects.Queries;
using Ctor.Domain.Entities;
using Ctor.Domain.Entities.Enums;

namespace Ctor.Application.Projects.Queries.GetProjectsQuery;

public class ProjectOverviewDto : IMapFrom<Domain.Entities.Project>
{
    public long Id { get; set; }
    public string ProjectName { get; set; }
    public string ProjectType { get; set; }
    public string Country { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string StartTime { get; set; }
    public string EndTime { get; set; }
    public string Status { get; set; }
    public List<PhaseOverviewDto> Phases { get; set; }
    public string? ImageUrl { get; set; }

    public virtual void Mapping(Profile profile)
    {
        profile.CreateMap<Project, ProjectOverviewDto>()
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ProjectPhotos.FirstOrDefault()!.Link))
            .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime.ToString("dd.MM.yyyy")))
            .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime.ToString("dd.MM.yyyy")))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.Phases, opt => opt.MapFrom(src => src.Phases.OrderBy(p => p.PhaseStep)));
    }
}
