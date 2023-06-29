using Ctor.Domain.Common;

namespace Ctor.Domain.Entities;
public class Company : BaseEntity
{
    public long CompanyId { get; set; }
    public string CompanyName { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public DateTime JoinDate { get; set; }
    public string? Website { get; set; }
    public string? Description { get; set; }
    public long? CompanyLogoId { get; set; }
    public CompanyLogo? CompanyLogo { get; set; }
    public virtual ICollection<Material> Materials { get; set; }
    public virtual ICollection<User> Users { get; set; }
    public virtual ICollection<Vendor> Vendors { get; set; }
    public virtual ICollection<Project> Projects { get; set; }
}
