using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Ctor.Application.Common.Exceptions;
using Ctor.Application.Common.Interfaces;
using Ctor.Application.DTOs.EmailDTos;
using Ctor.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using Ctor.Domain.Entities.Enums;

namespace Ctor.Application.Users.Commands;
public record AddSeveralUsersCommand:IRequest<IEnumerable<string>>
{
    public long CompanyId { get; set; }
    public Stream File { get; set; }
}
public class AddSeveralUsersCommandHandler : IRequestHandler<AddSeveralUsersCommand, IEnumerable<string>>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<AddSeveralUsersCommandHandler> _logger;
    private readonly IEmailService _email;
    private readonly IPasswordService _passwordGenerator;
    private readonly ISecurityService _securityService;
    private readonly ICsvFileService _csvFileService;

    public AddSeveralUsersCommandHandler(IApplicationDbContext context,
        ILogger<AddSeveralUsersCommandHandler> logger,
        IEmailService email,
        IPasswordService passwordGenerator,
        ISecurityService securityService,
        ICsvFileService parserService)
    {
        _context = context;
        _logger = logger;
        _email = email;
        _passwordGenerator = passwordGenerator;
        _securityService = securityService;
        _csvFileService = parserService;
    }

    public async Task<IEnumerable<string>> Handle(AddSeveralUsersCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Trying to add members...");
        if(request.File is null)
        {
            throw new FileNotFoundException("File was not loaded.");
        }

        List<string> errorLines = new List<string>();

        var members = _csvFileService.ReadMembers(request.File);
        errorLines.AddRange(_csvFileService.ErrorLines);

        List<User> users = new List<User>();
        var operationalManager = await _context.Users.SingleOrDefault(s =>
                    s.Role.Type == UserRoles.OperationalManager && s.CompanyId == request.CompanyId);
        var emails = new Dictionary<EmailDTO, string>();
        foreach (var member in members)
        {
            var role = await _context.Roles.SingleOrDefault(r => r.Name == member.RoleName);


            if (role.Type == UserRoles.OperationalManager && operationalManager is not null || role.Type==UserRoles.Admin)
            {
                errorLines.Add($"{member.FirstName},{member.LastName},{member.RoleName},{member.UserEmail}");
                continue;
            }
            string password = _passwordGenerator.GeneratePassword();
            emails.Add(
                new EmailDTO()
                {
                    Email = member.UserEmail,
                    Name = string.Format("{0} {1}", member.FirstName, member.LastName)
                }, password);
            string hashedPassword = password;//_securityService.ComputeSha256Hash(password);            
            users.Add(new User
            {
                FirstName = member.FirstName,
                LastName = member.LastName,
                UserEmail = member.UserEmail,
                CompanyId = request.CompanyId,
                Role=role,
                RoleId=role.Id,             
                Password=hashedPassword
            });            
        }
        await _context.Users.AddRangeAsync(users);
        await _context.SaveChangesAsync();
        foreach(var email in emails.Keys)
        {
            string password = emails.GetValueOrDefault(email);
            await _email.SendAsync(new List<EmailDTO>() {email}, "!", password, $"<h2>{password}</h2>");
        }
        return errorLines;
    }
}
