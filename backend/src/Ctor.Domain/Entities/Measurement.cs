using Ctor.Domain.Common;

namespace Ctor.Domain.Entities;

public class Measurement : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public ICollection<Material>? Materials { get; set; }
}