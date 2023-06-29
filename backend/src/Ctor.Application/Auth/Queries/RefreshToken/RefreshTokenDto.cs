namespace Ctor.Application.Auth.Queries.RefreshToken;

public class RefreshTokenDto
{
    public RefreshTokenDto(string token, DateTime expires)
    {
        Token = token;
        Expires = expires;
    }

    public string Token { get; }

    public DateTime Expires { get; }
}