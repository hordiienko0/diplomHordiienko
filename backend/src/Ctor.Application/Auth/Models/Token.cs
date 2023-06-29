namespace Ctor.Application.Auth.Models;

public struct Token
{
    public Token(string value, DateTime expiresAt)
    {
        Value = value;
        ExpiresAt = expiresAt;
    }

    public string Value { get; }

    public DateTime ExpiresAt { get; }
}