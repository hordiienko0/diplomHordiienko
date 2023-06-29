using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ctor.Application.Resources.Materials.Commands.CreateRequiredMaterialsForBuildingCommand;
public class CreateRequiredMaterialsForBuildingDto
{
    public long Id { get; set; }
    public long Amount { get; set; }
    public long MaxAmount  { get; set; }
    public string MaterialTypeName { get; set; }
    public string MeasurementName { get; set; }
    public long ProjectId { get; set; }
    public long BuildingId { get; set; }
    public decimal price { get; set; }
}
