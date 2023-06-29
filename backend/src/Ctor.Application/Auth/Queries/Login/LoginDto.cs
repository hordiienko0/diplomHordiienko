namespace Ctor.Application.Auth.Queries.Login;

public class LoginDto
{
    public LoginDto(LoginUserDto user, bool askToChangePassword, LoginTokenDto accessToken, LoginTokenDto refreshToken)
    {
        User = user;
        AskToChangePassword = askToChangePassword;
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }

    public LoginUserDto User { get; }

    public bool AskToChangePassword { get; }

    public LoginTokenDto AccessToken { get; }

    public LoginTokenDto RefreshToken { get; }
}

public class LoginUserDto
{
    public LoginUserDto(long id, string email)
    {
        Id = id;
        Email = email;
    }

    public long Id { get; }

    public string Email { get; }
}

public class LoginTokenDto
{
    public LoginTokenDto(string token, DateTime expires)
    {
        Token = token;
        Expires = expires;
    }

    public string Token { get; }

    public DateTime Expires { get; }
}