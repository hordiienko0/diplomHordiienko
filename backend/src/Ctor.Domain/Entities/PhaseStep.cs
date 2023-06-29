using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ctor.Domain.Common;

namespace Ctor.Domain.Entities;
public class PhaseStep : BaseEntity
{
    public string PhaseStepName { get; set; }
    public bool IsDone { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public long PhaseId { get; set; }
    public Phase Phase { get; set; }

    public long? BuildingId { get; set; }
    public Building Building { get; set; }
}
