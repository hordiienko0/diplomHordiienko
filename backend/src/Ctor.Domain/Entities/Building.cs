using Ctor.Domain.Common;
using Ctor.Domain.Entities.Enums;

namespace Ctor.Domain.Entities;
public class Building : BaseEntity
{
    public string BuildingName { get; set; }
    public virtual ICollection<BuildingBlock> BuildingBlocks { get; set; }
    public virtual ICollection<ProjectDocument> ProjectDocuments { get; set; }
    public ICollection<RequiredService> RequiredServices { get; set; }

    public long? ProjectId { get; set; }
    public Project Project { get; set; }
    
    public ICollection<RequiredMaterial> RequiredMaterials { get; set; }
}
