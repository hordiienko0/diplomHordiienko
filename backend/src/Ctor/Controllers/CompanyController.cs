using Microsoft.AspNetCore.Mvc;
using Ctor.Application.Companies.Queries.GetCompaniesOverview;
using Ctor.Application.Companies.Queries.GetCompanyById;
using Ctor.Application.Companies.Commands;
using Ctor.Application.Companies.Commands.DeleteCompanyLogo;
using Ctor.Application.Companies.Queries.GetCompanyByUserId;
using Ctor.Application.Companies.Commands.UpdateCompanyProfile;
using Ctor.Application.Companies.Queries.GetCompanyLogoByCompanyId;
using Ctor.Infrastructure.Extensions;
using Ctor.Application.Companies.Queries.GetNewCompanyId;
using Ctor.Application.DTOs;
using Ctor.Application.Companies.Queries.GetCompanyProjects;
using Ctor.Application.Common.Models;

namespace Ctor.Controllers;

[Route("api/companies")]
[ApiExceptionFilter]
public class CompanyController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<CompanyOverviewDto>>> GetCompanyWithParams([FromQuery(Name = "filter")] string? filter,
                                                                                   [FromQuery(Name = "sort")] string sort)
    {
        return await Mediator.Send(new GetCompaniesOverviewQuery(filter, sort));
    }

    [HttpGet]
    [Route("{id:long}/projects")]
    public async Task<ActionResult<PaginationModel<CompanyProjectDTO>>> GetCompanyProjects(long id, [FromQuery] PaginationQueryModelDTO dto)
    {
        return await Mediator.Send(new GetCompanyProjectsQuery(id, dto));
    }

    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> CreateCompany(NewCompanyDto model)
    {
        if (!ModelState.IsValid) return BadRequest("Model is not valid");
        await Mediator.Send(new CreateCompanyCommand(model));
        return StatusCode(201);
    }

    [HttpPut]
    public async Task<ActionResult<CompanyIdResponseDto>> PutCompany([FromBody]CompanyDetailedRequestDto data)
    {
        return await Mediator.Send(new PutCompanyDetailedCommand(data));
    }

    [HttpGet("{id:long}")]
    public async Task<ActionResult<CompanyDetailedResponseDto>> GetCompanyById(long id)
    {
        return await Mediator.Send(new GetCompanyByIdQuery(id));
    }

    [HttpGet("get-by-user-id/{userId:long}")]
    public async Task<ActionResult<CompanyProfileDto>> GetCompanyByUserId(long userId)
    {
        return await Mediator.Send(new GetCompanyByUserIdQuery(userId));
    }

    [HttpPut("company-profile/{id:long}")]
    public async Task<ActionResult<CompanyProfileUpdatedDto>> UpdateCompanyProfile(long id, [FromBody] UpdateCompanyProfileCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }
        return await Mediator.Send(command);
    }

    [HttpGet("{id:long}/logo")]
    public async Task<ActionResult<GetCompanyLogoByCompanyIdResponseDto>> GetCompanyLogo(long id)
    {
        return await Mediator.Send(new GetCompanyLogoByCompanyIdQuery(id));
    }

    [HttpDelete("{id:long}/logo")]
    public async Task<ActionResult<DeleteCompanyLogoResponseDto>> DeleteCompanyLogo(long id)
    {
        return await Mediator.Send(new DeleteCompanyLogoCommand(id));
    }

    [HttpPut("{id:long}/logo")]
    public async Task<ActionResult<PutCompanyLogoResponseDto>> PutCompanyLogo([FromForm]IFormFile data, long id)
    {
        return await Mediator.Send(new PutCompanyLogoCommand(await data.GetBytes(), id, Path.GetExtension(data.FileName)));
    }

    [HttpGet("newCompanyId")]
    public async Task<ActionResult<long>> GetNewCompanyGeneratedId()
    {
        return await Mediator.Send(new GetNewCompanyIdQuery());
    }
}
