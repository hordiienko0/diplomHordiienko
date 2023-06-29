using Ctor.Domain.Common;
using Ctor.Domain.Entities.Enums;

namespace Ctor.Domain.Entities;

public class Project : BaseEntity
{
    public int ProjectId { get; set; }
    public string ProjectName { get; set; }
    public string ProjectType { get; set; }
    public string Country { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public decimal Budget { get; set; }
    public string? Description { get; set; }
    public ProjectStatus Status { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public virtual ICollection<Building> Building { get; set; }
    public virtual ICollection<Phase> Phases { get; set; }
    public virtual ICollection<ProjectDocument> ProjectDocuments { get; set; }
    public virtual ICollection<ProjectNote> ProjectNote { get; set; }
    public virtual ICollection<ProjectPhoto> ProjectPhotos { get; set; }
    public ICollection<Assignee> Assignees { get; set; }

    public long CompanyId { get; set; }
    public Company Company { get; set; }

    public long? UserId { get; set; }
    public User? User { get; set; }

}