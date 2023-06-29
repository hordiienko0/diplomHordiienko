using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ctor.Application.Resources.Materials.Queries.GetAvailableMaterialsForProjectQuery;
public class GetAvailableMaterialsForProjectDto
{
    public long Id { get; set; }
    public long Amount { get; set; }
    public string CompanyName { get; set; } = string.Empty;
    public string CompanyAddress { get; set; } = string.Empty;
    public string MaterialTypeName { get; set; }
    public string MeasurementName { get; set; }
    public decimal Price { get; set; }

}
