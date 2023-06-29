using Ctor.Application.Common.Mapping;
using Ctor.Domain.Entities;

namespace Ctor.Application.Phases.Queries.GetPhasesByProjectId;

public class PhaseStepDto : IMapFrom<PhaseStep>
{
    public long Id { get; set; }
    public string PhaseStepName { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public long? BuildingId { get; set; }
    public bool IsDone { get; set; }
}