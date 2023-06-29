using Ctor.Domain.Common;
using Ctor.Domain.Entities.Enums;

namespace Ctor.Domain.Entities;

public class ProjectPhoto : BaseEntity
{
    public string Path { get; set; }
    public FileProviderType Type { get; set; }
    public string Link { get; set; }

    public long ProjectId { get; set; }
    public Project Project { get; set; }

}