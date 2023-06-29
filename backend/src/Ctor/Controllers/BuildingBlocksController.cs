using Ctor.Application.BuildingBlocks.Commands.AddBuildingBlock;
using Ctor.Application.BuildingBlocks.Commands.DeleteBuildingBlock;
using Ctor.Application.BuildingBlocks.Commands.UpdateBuildingBlock;
using Microsoft.AspNetCore.Mvc;

namespace Ctor.Controllers;

[Route("api/building-blocks")]
public class BuildingBlocksController : ApiControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddBuildingBlock([FromBody] AddBuildingBlockCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> UpdateBuildingBlock(long id, [FromBody] UpdateBuildingBlockCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }
        await Mediator.Send(command);
        return Ok();
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeleteBuildingBlock(long id)
    {
        await Mediator.Send(new DeleteBuildingBlockCommand(id));
        return Ok();
    }
}
