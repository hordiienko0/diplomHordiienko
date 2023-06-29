using Ctor.Application.Common.Interfaces;
using Ctor.Application.DTOs.EmailDTos;
using Ctor.Application.MyEntity.Commands;
using Ctor.Application.MyEntity.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ctor.Controllers;

public class MyEntityController : ApiControllerBase
{
    private readonly IEmailService _emailService;

    public MyEntityController(IEmailService emailService)
    {
        _emailService = emailService;
    }
    
    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<MyEntityDto>> Get(int id)
    {
       
       
        return await Mediator.Send(new GetMyEntitiesQuery(id));
    }

    [HttpGet]
    [Route("sendMail")]
    public async Task<ActionResult<MyEntityDto>> SendMail(int id)
    {
        var emailDTOs = new List<EmailDTO>() {
            new EmailDTO(){
                Email="vitalikkravez1@gmail.com",
                Name="Vitalik"
            }
        };
        await _emailService.SendAsync(emailDTOs, "Test", "Text", "<h2>Hello my brother</h2>");
        return NoContent();
    }

    [HttpPost]
    [Route("SendBusMessage")]
    public async Task<ActionResult<Unit>> SendBusMessage()
    {
        return await Mediator.Send(new BusTestCommand());
    }

    [HttpPost]
    [Route("SendAzureServiceBusMessage")]
    public async Task<ActionResult<Unit>> SendAzureServiceBusMessage()
    {
        return await Mediator.Send(new AzureServiceBusCommand());
    }
}
