using AutoMapper;
using Ctor.Application.Common.Mapping;
using Ctor.Application.Projects.Queries;
using Ctor.Domain.Entities;
using Ctor.Domain.Entities.Enums;

namespace Ctor.Application.Projects.Queries.GetProjectsQuery;

public class ProjectDetailedDTO : ProjectOverviewDto, IMapFrom<Project>
{

    public override void Mapping(Profile profile)
    {
        profile.CreateMap<Project, ProjectDetailedDTO>()
            .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime.ToString()))
            .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime.ToString()))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.Phases, opt => opt.MapFrom(src => src.Phases.OrderBy(p => p.PhaseStep)));
    }
}
