using Ctor.Application.Common.Mapping;
using Ctor.Domain.Entities;

namespace Ctor.Application.Roles.Queries;
public class RoleDto : IMapFrom<Role>
{
    public int Id { get; set; }
    public string Name { get; set; }
}

