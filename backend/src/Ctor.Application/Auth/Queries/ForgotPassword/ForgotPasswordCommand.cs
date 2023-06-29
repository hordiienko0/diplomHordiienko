using Ctor.Application.Common.Exceptions;
using Ctor.Application.Common.Interfaces;
using Ctor.Application.DTOs.EmailDTos;
using MediatR;

namespace Ctor.Application.Auth.Queries.ForgotPassword;

public record ForgotPasswordCommand(string Email) : IRequest<ForgotPasswordDto>;

public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, ForgotPasswordDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IEmailService _emailService;
    private readonly IPasswordService _passwordService;
    private readonly ISecurityService _securityService;

    public ForgotPasswordCommandHandler(
        IApplicationDbContext context,
        IEmailService emailService,
        IPasswordService passwordService,
        ISecurityService securityService)
    {
        _context = context;
        _emailService = emailService;
        _passwordService = passwordService;
        _securityService = securityService;
    }

    public async Task<ForgotPasswordDto> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.SingleOrDefault(x => x.UserEmail == request.Email);
        if (user == null)
        {
            throw new NotFoundException("Email was not found");
        }

        var password = _passwordService.GeneratePassword();
        user.Password = _securityService.ComputeSha256Hash(password);
        _context.Users.Update(user);
        await _context.SaveChangesAsync(cancellationToken);

        var emails = new List<EmailDTO>()
        {
            new() { Email = request.Email, Name = $"{user.FirstName} {user.LastName}" }
        };

        var html = $"<h2>Forgot Password</h2> <p>{password}</p>";
        await _emailService.SendAsync(emails, "Forgot Password", "Forgot Password", html);

        return new ForgotPasswordDto();
    }
}