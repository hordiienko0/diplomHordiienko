using Ctor.Domain.Common;

namespace Ctor.Domain.Entities;

public class Assignee : BaseEntity
{
    public User User { get; set; }

    public Project Project { get; set; }
}