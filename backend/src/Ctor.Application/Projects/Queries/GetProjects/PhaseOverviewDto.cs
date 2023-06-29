using AutoMapper;
using Ctor.Application.Common.Mapping;
using Ctor.Domain.Entities;

namespace Ctor.Application.Projects.Queries.GetProjectsQuery;
public class PhaseOverviewDto : IMapFrom<Phase>
{
    public long Id { get; set; }
    public string PhaseName { get; set; }
    public bool IsFinished { get; set; }
    public int PhaseStep { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Phase, PhaseOverviewDto>()
            .ForMember(dest => dest.IsFinished, opt => opt.MapFrom(src => src.EndTime < DateTime.UtcNow));
    }


}
