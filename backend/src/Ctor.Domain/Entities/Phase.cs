using Ctor.Domain.Common;

namespace Ctor.Domain.Entities;
public class Phase : BaseEntity
{
    public string PhaseName { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int PhaseStep { get; set; }
    

    public long ProjectId { get; set; }
    public Project Project { get; set; }

    public ICollection<PhaseStep> PhaseSteps { get; set; }
}
