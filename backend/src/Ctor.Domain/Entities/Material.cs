using Ctor.Domain.Common;

namespace Ctor.Domain.Entities;
public class Material: BaseEntity
{
    public long? MaterialTypeId { get; set; }
    public MaterialType? MaterialType { get; set; }
    public string CompanyName { get; set; } = string.Empty;
    public string CompanyAddress { get; set; } = string.Empty;
    public long? MeasurementId { get; set; }
    public Measurement? Measurement { get; set; }
    public decimal Price { get; set; }
    public int Amount { get; set; }
    public DateTime Date { get; set; }

    public long? CompanyId { get; set; }
    public Company Company { get; set; }
    
    public ICollection<RequiredMaterial> RequiredMaterials { get; set; }
}
