using AutoMapper;
using Ctor.Application.Common.Mapping;
using Ctor.Domain.Entities;

namespace Ctor.Application.Phases.Queries.GetPhasesByProjectId;

public class PhaseDto : IMapFrom<Phase>
{
    public long Id { get; set; }
    public string PhaseName { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int Progress { get; set; }
    public ICollection<PhaseStepDto> PhaseSteps { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Phase, PhaseDto>()
            .ForMember(dest => dest.PhaseSteps, opt => opt.MapFrom(src => src.PhaseSteps.OrderBy(steps => steps.StartDate)))
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartTime))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndTime))
            .ForMember(dest => dest.Progress, opt => opt.MapFrom(src => src.PhaseSteps.Count == 0 ? 0 : (int)Math.Round(src.PhaseSteps.Count(x => x.IsDone) * 100d / src.PhaseSteps.Count)));
    }
}