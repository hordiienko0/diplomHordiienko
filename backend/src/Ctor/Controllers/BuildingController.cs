using Ctor.Application.Buildings.Commands.AddBuilding;
using Ctor.Application.Buildings.Commands.DeleteBuilding;
using Ctor.Application.Buildings.Commands.UpdateBuilding;
using Ctor.Application.Buildings.Queries.GetBuildingsListByProjectId;
using Microsoft.AspNetCore.Mvc;

namespace Ctor.Controllers;
[Route("/api/buildings")]
public class BuildingController : ApiControllerBase
{
    [HttpGet("project/{projectId:long}")]
    public async Task<ActionResult<List<BuildingDto>>> GetBuildingByProjectId(long projectId)
    {
        return await Mediator.Send(new GetBuildingsListByProjectIdQuery(projectId));
    }

    [HttpPost]
    public async Task<IActionResult> AddBuilding([FromBody] AddBuildingCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> UpdateBuilding(long id, [FromBody] UpdateBuildingCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(command);
        return Ok();
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeleteBuilding(long id)
    {
        await Mediator.Send(new DeleteBuildingCommand(id));
        return Ok();
    }
}
