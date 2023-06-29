using System.Security.Claims;
using Ctor.Application.Auth.Interfaces;
using Ctor.Application.Auth.Models;
using Ctor.Application.Common.Exceptions;
using Ctor.Application.Common.Interfaces;
using Ctor.Domain.Entities;
using Ctor.Domain.Repositories;

namespace Ctor.Infrastructure.Services;

public class UserManager : IUserManager
{
    private readonly ITokenProvider _tokenProvider;
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly IUserRepository _userRepository;

    public UserManager(ITokenProvider tokenProvider, IApplicationDbContext applicationDbContext)
    {
        _tokenProvider = tokenProvider;
        _applicationDbContext = applicationDbContext;
        _userRepository = applicationDbContext.Users;
    }

    public async Task<LoginResult> LoginAsync(
        string email,
        string password,
        CancellationToken ct)
    {
        var user = await _userRepository.GetByEmailAsync(email, ct);

        EnsurePasswordIsCorrect(user, password);

        var accessToken = _tokenProvider.GenerateAccessToken(user.Id, user.Role.Type, user.CompanyId);
        var refreshToken = _tokenProvider.GenerateRefreshToken(user.Id);

        user.RefreshToken = refreshToken.Value;
        user.RefreshTokenExpiresAt = refreshToken.ExpiresAt;

        await _applicationDbContext.SaveChangesAsync(ct);

        return new LoginResult(user.Id, user.UserEmail, user.AskToChangeDefaultPassword, accessToken, refreshToken);
    }

    public async Task LogoutAsync(long userId, CancellationToken ct)
    {
        var user = await _userRepository.GetById(userId, ct);

        user.RefreshToken = null;
        user.RefreshTokenExpiresAt = null;

        await _applicationDbContext.SaveChangesAsync(ct);
    }

    public async Task<Token> RefreshAccessTokenAsync(string accessToken, string refreshToken, CancellationToken ct)
    {
        var principal = _tokenProvider.GetPrincipalFromExpiredAccessToken(accessToken);

        if (principal == null)
        {
            throw new NotFoundException("User was not found");
        }

        var user = await GetUserAsync(principal, ct);

        if (user.RefreshToken == null ||
            !user.RefreshTokenExpiresAt.HasValue ||
            !user.RefreshToken.Equals(refreshToken, StringComparison.Ordinal) ||
            user.RefreshTokenExpiresAt < DateTime.UtcNow)
        {
            throw new UnauthorizedAccessException();
        }

        return _tokenProvider.GenerateAccessToken(user.Id, user.Role.Type, user.CompanyId);
    }

    public async Task KeepDefaultPasswordAsync(long userId, CancellationToken ct)
    {
        var user = await _userRepository.GetById(userId, ct);

        user.AskToChangeDefaultPassword = false;

        await _applicationDbContext.SaveChangesAsync(ct);
    }

    public async Task ChangeDefaultPasswordAsync(long userId, string newPassword, CancellationToken ct)
    {
        var user = await _userRepository.GetById(userId, ct);

        if (!user.AskToChangeDefaultPassword)
        {
            throw new ForbiddenAccessException();
        }

        // todo: salt and hash

        user.Password = newPassword;
        user.AskToChangeDefaultPassword = false;

        await _applicationDbContext.SaveChangesAsync(ct);
    }

    public async Task ChangePasswordAsync(
        long userId,
        string currentPassword,
        string newPassword,
        CancellationToken ct)
    {
        var user = await _userRepository.GetById(userId, ct);

        EnsurePasswordIsCorrect(user, currentPassword);

        // todo: salt and hash

        user.Password = newPassword;
        user.AskToChangeDefaultPassword = false;

        await _applicationDbContext.SaveChangesAsync(ct);
    }

    private async Task<User> GetUserAsync(ClaimsPrincipal principal, CancellationToken ct)
    {
        var idClaim = principal.FindFirstValue("id");

        if (!long.TryParse(idClaim, out long id))
        {
            throw new NotFoundException("User was not found");
        }

        return await GetUserAsync(id, ct);
    }

    private async Task<User> GetUserAsync(long id, CancellationToken ct)
    {
        return await _userRepository.GetById(id, ct);
    }

    private void EnsurePasswordIsCorrect(User user, string password)
    {
        // todo: salt and hash

        if (!user.Password.Equals(password, StringComparison.Ordinal))
        {
            throw new UnauthorizedAccessException();
        }
    }
}