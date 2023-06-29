using Ctor.Application.Companies.Commands;
using Ctor.Application.DTOs;
using Ctor.Application.Projects.Commands;
using Ctor.Application.Projects.Commands.ChangeStatus;
using Ctor.Application.Projects.Queries;
using Ctor.Application.Projects.Queries.GetProjectsByCompanyId;
using Ctor.Application.Projects.Queries.GetProjectsQuery;
using Ctor.Application.Projects.Commands.CreateProjectCommand;
using Ctor.Application.Projects.Commands.SetProjectTeam;
using Ctor.Application.Projects.Queries.GetProjectTeam;
using Ctor.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ctor.Controllers;

[Authorize]
[Route("api/projects")]
public class ProjectController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult> GetProjectsWithParams([FromQuery] ProjectPaginationQueryDTO queryModel)
    {
        return Ok(await Mediator.Send(new GetProjectsOverviewQuery(queryModel)));
    }

    [HttpGet("{id:long}")]
    public async Task<ActionResult> GetProjectDetailed(long id)
    {
        return Ok(await Mediator.Send(new GetProjectDetailedQuery(id)));
    }

    [HttpPut]
    public async Task<ActionResult> PutProject([FromBody] ProjectDetailedRequestDto project)
    {
        return Ok(await Mediator.Send(new PutProjectDetailedCommand(project)));
    }

    [HttpPost("")]
    public async Task<IActionResult> CreateProjects([FromBody] CreateProjectDTO project)
    {
        return Ok(await Mediator.Send(new CreateProjectCommand(project)));
    }

    [HttpGet("company/{id}")]
    public async Task<ActionResult<List<ProjectOverviewDto>>> GetProjectsByCompanyId([FromRoute] long id)
    {
        return await Mediator.Send(new GetProjectsByCompanyIdQuery(id));
    }

    [HttpGet("{id:long}/photos")]
    public async Task<ActionResult<List<GetProjectPhotoByProjectIdResponseDto>>> GetProjectPhotos(
        long id)
    {
        return await Mediator.Send(
            new GetProjectPhotosByProjectIdQuery(id));
    }

    [HttpPut("{id:long}/photos")]
    public async Task<ActionResult<List<PutProjectPhotoResponseDto>>> PutProjectPhotos(
        [FromForm] IFormCollection data, long id)
    {
        return await Mediator.Send(
            new PutProjectPhotosCommand(await Task.WhenAll(data.Files.Select(file => file.GetBytes())), id));
    }

    [HttpDelete("{id:long}/photos/{photoId:long}")]
    public async Task<ActionResult<DeleteProjectPhotoByIdResponseDto>> DeleteProjectPhotoById(long id, long photoId)
    {
        return await Mediator.Send(new DeleteProjectPhotoByIdCommand(id, photoId));
    }

    [HttpPut("change-status")]
    public async Task<IActionResult> ChangeStatus([FromBody] ChangeStatusCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpGet("team")]
    public async Task<IActionResult> GetTeam([FromQuery] GetProjectTeamQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost("team")]
    public async Task<IActionResult> SetTeam([FromBody] SetProjectTeamCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}