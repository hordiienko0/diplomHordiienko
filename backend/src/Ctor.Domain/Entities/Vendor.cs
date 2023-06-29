using Ctor.Domain.Common;

namespace Ctor.Domain.Entities;
public class Vendor: BaseEntity
{
    public string VendorName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Website { get; set; }

    public long? CompanyId { get; set; }
    public Company Company { get; set; }
    
    public ICollection<RequiredService> RequiredServices { get; set; }
    public virtual ICollection<VendorType> VendorTypes { get; set; }
}
