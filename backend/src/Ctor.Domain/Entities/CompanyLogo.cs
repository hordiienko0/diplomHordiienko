using Ctor.Domain.Common;
using Ctor.Domain.Entities.Enums;

namespace Ctor.Domain.Entities;

public class CompanyLogo : BaseEntity
{
    public string Path { get; set; }
    public FileProviderType Type { get; set; }
    public string Link { get; set; }

    public long CompanyId { get; set; }
    public Company Company { get; set; }

}