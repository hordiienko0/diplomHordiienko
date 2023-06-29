namespace Ctor.Application.Auth.Models;

public record LoginResult(
    long UserId,
    string Email,
    bool AskToChangePassword,
    Token AccessToken,
    Token RefreshToken);