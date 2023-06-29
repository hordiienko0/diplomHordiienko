using Ctor.Domain.Common;

namespace Ctor.Domain.Entities;

public class RequiredMaterial : BaseEntity
{
    public long BuildingId { get; set; }
    public Building Building { get; set; }
    public long MaterialId { get; set; }
    public Material Material { get; set; }
    public long Amount { get; set; }
}