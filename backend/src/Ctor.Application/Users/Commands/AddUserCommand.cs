using Ctor.Application.Common.Exceptions;
using Ctor.Application.Common.Interfaces;
using Ctor.Application.DTOs.EmailDTos;
using Ctor.Application.MyEntity.Queries;
using Ctor.Domain.Entities;
using Ctor.Domain.Entities.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ctor.Application.Users.Commands;
public record AddUserCommand : IRequest<bool>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserEmail { get; set; }
    public string RoleName { get; set; }
    public int CompanyId { get; set; }
}
public class AddUserQueryHandler : IRequestHandler<AddUserCommand,bool>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<GetMyEntitiesQueryHandler> _logger;
    private readonly IEmailService _email;
    private readonly IPasswordService _passwordGenerator;
    private readonly ISecurityService _securityService;

    public AddUserQueryHandler(IApplicationDbContext context, ILogger<GetMyEntitiesQueryHandler> logger, IEmailService email, IPasswordService passwordGenerator, ISecurityService securityService)
    {
        _context = context;
        _logger = logger;
        _email = email;
        _passwordGenerator = passwordGenerator;
        _securityService = securityService;
    }

    public async Task<bool> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Trying to add member...");
        User newUser = new User()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            UserEmail = request.UserEmail,
            CompanyId = request.CompanyId
        };
        var role = await _context.Roles.SingleOrDefault(r => r.Name == request.RoleName);

        if (request.RoleName == "Operational manager")
        {

            var operationalManager = await _context.Users.SingleOrDefault(s =>
                    s.Role.Type == UserRoles.OperationalManager && s.CompanyId == request.CompanyId);

            if (operationalManager is not null)
            {
                throw new AlreadyExistsException("Operational manger has already exist.");
            }
        }
        newUser.Role = role;
        newUser.RoleId = role.Id;
        string password = _passwordGenerator.GeneratePassword();
        var emailDTOs = new List<EmailDTO>() {
            new EmailDTO(){
                Email = newUser.UserEmail,
                Name = string.Format("{0} {1}", newUser.FirstName, newUser.LastName)
            }
        };
        newUser.Password = password; //_securityService.ComputeSha256Hash(password);        
        await _context.Users.Insert(newUser);
        await _context.SaveChangesAsync();
        await _email.SendAsync(emailDTOs, "Password for your new account!", password, $"<h2>{password}</h2>");
        return true;
    }
}
