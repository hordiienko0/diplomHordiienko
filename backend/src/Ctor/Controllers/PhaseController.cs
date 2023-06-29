using Ctor.Application.Phases.Commands.AddPhase;
using Ctor.Application.Phases.Commands.DeletePhase;
using Ctor.Application.Phases.Commands.UpdatePhase;
using Ctor.Application.Phases.Commands.UpdatePhaseStep;
using Ctor.Application.Phases.Commands.UpdatePhaseSteps;
using Ctor.Application.Phases.Queries.GetPhasesByProjectId;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ctor.Controllers;

[Route("api/phases")]
public class PhaseController : ApiControllerBase
{
    [HttpGet("project/{projectId:long}")]
    public async Task<List<PhaseDto>> GetPhasesByProjectId(long projectId)
    {
        return await Mediator.Send(new GetPhasesByProjectIdQuery(projectId));
    }

    [HttpPost]
    public async Task<IActionResult> PostPhase([FromBody] AddPhaseCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> PutPhase(long id, [FromBody] UpdatePhaseCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(command);
        return Ok();
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeletePhase(long id)
    {
        await Mediator.Send(new DeletePhaseCommand(id));
        return Ok();
    }

    [HttpPut("steps")]
    public async Task<IActionResult> EditPhaseSteps([FromBody] UpdatePhaseStepsCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }

    [HttpPut("steps/{id:long}")]
    public async Task<IActionResult> EditPhaseStep(long id, [FromBody] UpdatePhaseStepCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }
}
