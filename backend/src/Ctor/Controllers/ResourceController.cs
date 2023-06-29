using Ctor.Application.Common.Models;
using Ctor.Application.Resources.Commands.DeleteRequiredMaterial;
using Ctor.Application.Resources.Materials.Commands.CreateMaterialCommand;
using Ctor.Application.Resources.Materials.Commands.CreateMaterialReport;
using Ctor.Application.Resources.Materials.Commands.DeleteMaterialCommand;
using Ctor.Application.Resources.Materials.Commands.PutMaterialCommand;
using Ctor.Application.Resources.Materials.Commands.CreateRequiredMaterialsForBuildingCommand;
using Ctor.Application.Resources.Materials.Queries.GetAvailableMaterialsForProjectQuery;
using Ctor.Application.Resources.Materials.Queries.GetMaterialTypeQuery;
using Ctor.Application.Resources.Materials.Queries.GetMeasurementQuery;
using Ctor.Application.Resources.Queries.GetMaterialQuery;
using Ctor.Application.Resources.Materials.Queries.GetAllRequiredMaterialsForProject;
using Ctor.Application.Resources.Queries.GetRequiredMaterials;
using Ctor.Application.Services;
using Ctor.Application.Services.Commands;
using Ctor.Application.Services.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ctor.Controllers;

[ApiController]
[Authorize]
[Route("api/resources")]
public class ResourceController : ApiControllerBase
{
    [HttpPost]
    [Route("service/add")]
    public async Task<ActionResult<ServiceDto>> AddService([FromBody] AddServiceCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpDelete]
    [Route("service/{id}/delete")]
    public async Task<ActionResult<long>> DeleteService([FromRoute] long id)
    {
        return await Mediator.Send(new DeleteServiceCommand { Id = id });
    }

    [HttpPut]
    [Route("service/edit")]
    public async Task<ActionResult<ServiceDto>> EditService([FromBody] EditServiceCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpGet]
    [Route("services")]
    public async Task<ActionResult<IEnumerable<ServiceDto>>> GetCompanyWithParams([FromQuery(Name = "filter")] string? filter,
                                                                                   [FromQuery(Name = "sort")] string sort)
    {
        return Ok(await Mediator.Send(new GetServicesQuery(filter, sort)));
    }

    [HttpGet]
    [Route("types")]
    public async Task<ActionResult<IEnumerable<ServiceDto>>> LoadTypes()
    {
        return Ok(await Mediator.Send(new GetTypesQuery()));
    }
    [HttpGet("materials")]
    public async Task<ActionResult<PaginationModel<GetMaterialsQueryDto>>> GetListOfMaterials(
        [FromQuery] MaterialPaginationQueryDto queryModel)
    {
        return await Mediator.Send(new GetMaterialsQuery(queryModel));
    }
    [HttpGet("material/get-material-type")]
    public async Task<ActionResult> GetListOfMaterialType()
    {
        return Ok(await Mediator.Send(new GetMaterialTypeQuery()));
    }
    [HttpGet("material/get-measurement")]
    public async Task<ActionResult> GetListOfMeasurement()
    {
        return Ok(await Mediator.Send(new GetMeasurementQuery()));
    }

    [HttpPost]
    [Route("material/create")]
    public async Task<IActionResult> CreateMatial(CreateMaterialCommandDto model)
    {
        if (!ModelState.IsValid) return BadRequest("Model is not valid");
        await Mediator.Send(new CreateMaterialCommand(model));
        return StatusCode(201);
    }
    [HttpDelete("material/{id:long}")]
    public async Task<ActionResult<DeleteMaterialByIdCommandDto>> DeleteMaterialById(long id)
    {
        if (!ModelState.IsValid) return BadRequest("Model is not valid");

        return await Mediator.Send(new DeleteMaterialByIdCommand(id));
    }
    [HttpPut("material/edit")]
    public async Task<ActionResult> PutMaterial([FromBody] PutMaterialCommand command)
    {
        return Ok(await Mediator.Send(command));

    }
    [HttpGet]
    [Route("available-materials")]
    public async Task<IActionResult> GetAvailableMaterialsForProject(
        [FromQuery] GetAvailableMaterialsForProjectQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("required-materials")]
    public async Task<ActionResult<GetRequiredMaterialDto>> GetRequiredMaterials(
        [FromQuery] GetRequiredMaterialsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpDelete("required-materials/{id}")]
    public async Task<ActionResult<Unit>> DeleteRequiredMaterial([FromRoute] long id)
    {
        return Ok(await Mediator.Send(new DeleteRequiredMaterialCommand(id)));
    }

    [HttpPost]
    [Route("save-required")]
    public async Task<IActionResult> SaveRequiredMaterials([FromBody] CreateRequiredMaterialsForBuildingDto[] materials)
    {
        await Mediator.Send(new CreateRequiredMaterialsForBuildingCommand(materials));
        return StatusCode(201);

    }

    [HttpPost("create-report/{projectId:long}")]
    public async Task<ActionResult> CreateReportForProject(long projectId)
    {
        return Ok(await Mediator.Send(new CreateMaterialReportCommand(projectId)));
    }

    [HttpGet]
    [Route("materials-of-project")]
    public async Task<ActionResult<List<GetAllRequiredMaterialsForProjectDto>>> GetAllRequiredMaterialsForProject([FromQuery] GetAllRequiredMaterialsForProjectQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet]
    [Route("available-services/{buildingId:long}")]
    public async Task<ActionResult<IEnumerable<ServiceDto>>> GetServices(long buildingId, [FromQuery(Name = "filter")] string? filter )
    {
        return await Mediator.Send(new GetRequiredServicesQuery(filter,buildingId));
    }
    [HttpGet]
    [Route("selected-services/{buildingId:long}")]
    public async Task<ActionResult<IEnumerable<ServiceDto>>> GetSelectedServices(long buildingId)
    {
        return await Mediator.Send(new GetSelectedServicesQuery(buildingId));
    }
    [HttpPost]
    [Route("add/services")]
    public async Task<ActionResult<List<ServiceDto>>> AddRequiredServices([FromBody]AddServiceToBuildingCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpDelete]
    [Route("building/{buildingId:long}/delete-service/{serviceId:long}")]
    public async Task<IActionResult> DeleteRequiredService(long serviceId, long buildingId)
    {
        await Mediator.Send(new DeleteRequiredServiceCommand(buildingId, serviceId));
        return Ok();
    }
}