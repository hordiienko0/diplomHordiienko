using Ctor.Domain.Common;

namespace Ctor.Domain.Entities;
public class BuildingBlock : BaseEntity
{
    public string BuildingBlockName { get; set; }
    public string Details { get; set; }
    public bool IsDone { get; set; }

    public long? BuildingId { get; set; }
    public Building Building { get; set; }

}
