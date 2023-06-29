namespace Ctor.Application.Resources.Materials.Queries.GetAllRequiredMaterialsForProject;
public class GetAllRequiredMaterialsForProjectDto
{
    public long BuildingId { get; set; }
    public string BuildingName { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string CompanyAddress { get; set; } = string.Empty;
    public long MaterialId { get; set; }
    public string MaterialTypeName { get; set; } = string.Empty;
    public string MeasurementName { get; set; } = string.Empty;
    public long Amount { get; set; }
    public decimal Price { get; set; }
}
