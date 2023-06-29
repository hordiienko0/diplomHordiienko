using Ctor.Domain.Common;

namespace Ctor.Domain.Entities;
public class ProjectNote : BaseEntity
{
    public DateTime Date { get; set; }
    public string Text { get; set; }

    public long? UserId { get; set; }
    public User User { get; set; }
    public long? ProjectId { get; set; }
    public Project Project { get; set; }
}
