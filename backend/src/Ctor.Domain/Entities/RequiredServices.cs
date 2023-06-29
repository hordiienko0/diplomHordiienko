using Ctor.Domain.Common;

namespace Ctor.Domain.Entities;

public class RequiredService : BaseEntity
{
    public long BuildingId { get; set; }
    public Building Building { get; set; } = null!;

    public long VendorId { get; set; }
    public Vendor Vendor { get; set; } = null!;
}