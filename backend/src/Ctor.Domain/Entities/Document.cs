using Ctor.Domain.Common;

namespace Ctor.Domain.Entities;
public class Document : BaseEntity
{
    public string Name { get; set; }
    public string Path { get; set; }
    public string Link { get; set; }
    public virtual ProjectDocument? ProjectDocument { get; set; }
    public long? ProjectDocumentId { get; set; }

}
