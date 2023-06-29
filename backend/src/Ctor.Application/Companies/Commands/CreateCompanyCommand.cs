using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ctor.Application.Common.Exceptions;
using Ctor.Application.Common.Interfaces;
using Ctor.Domain.Entities;
using Ctor.Domain.Entities.Enums;
using Ctor.Domain.Repositories;
using MediatR;

namespace Ctor.Application.Companies.Commands;
public record CreateCompanyCommand : IRequest
{
    public CreateCompanyCommand(NewCompanyDto model)
    {
        this.Model = model;
    }
    public NewCompanyDto Model { get; }
}

public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly INotificationService _notifService;
    private ICurrentUserService _currentUserService;
    public CreateCompanyCommandHandler(IApplicationDbContext context, INotificationService notifService, ICurrentUserService currentUserService)
    {
        _context = context;
        _notifService = notifService;
        _currentUserService = currentUserService;
    }

    public async Task<Unit> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
        List<Company> companies = await _context.Companies.GetAll();
        Company newCompany = new Company()
        {
            CompanyId = request.Model.CompanyId,
            Email = request.Model.Email,
            Address = request.Model.Address,
            City = request.Model.City,
            CompanyName = request.Model.CompanyName,
            Country = request.Model.Country,
            JoinDate = DateTime.UtcNow
        };
        if (companies.Any(el => el.CompanyId == newCompany.CompanyId)) throw new AlreadyExistsException("Company with this CompanyId already exist.");
        await _context.Companies.Insert(newCompany);
        await _context.SaveChangesAsync();

        // ----- Sending notification to frontend

        await _notifService.SendNotificationToUser(new DTOs.NotificationDTO()
        {
            Message = "Company succesfully created",
            type = NotificationTypes.Success
        }, _currentUserService.Id);

        await _notifService.SendNotificationToGroup(new DTOs.NotificationDTO()
        {
            Message = "Company " + newCompany.CompanyName + " was just created",
            type = NotificationTypes.Info
        }, "admin");

        // -----

        return Unit.Value;
    }
}


