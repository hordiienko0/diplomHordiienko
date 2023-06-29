using Ctor.Domain.Common;
using Ctor.Domain.Entities.Enums;

namespace Ctor.Domain.Entities;

public class Role : BaseEntity
{
    public string Name { get; set; } = null!;
    public UserRoles Type { get; set; }

    public ICollection<User> Users { get; set; } = null!;
}
