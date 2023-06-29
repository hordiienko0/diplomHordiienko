using Ctor.Application.Auth.Models;

namespace Ctor.Application.Auth.Interfaces;

public interface IUserManager
{
    Task<LoginResult> LoginAsync(string email, string password, CancellationToken ct);

    Task LogoutAsync(long userId, CancellationToken ct);

    Task<Token> RefreshAccessTokenAsync(string accessToken, string refreshToken, CancellationToken ct);

    Task KeepDefaultPasswordAsync(long userId, CancellationToken ct);

    Task ChangeDefaultPasswordAsync(long userId, string newPassword, CancellationToken ct);

    Task ChangePasswordAsync(long userId, string currentPassword, string newPassword, CancellationToken ct);
}