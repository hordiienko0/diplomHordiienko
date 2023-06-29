using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Ctor.Application.Auth.Interfaces;
using Ctor.Application.Auth.Models;
using Ctor.Application.Common.Interfaces;
using Ctor.Domain.Entities.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Ctor.Infrastructure.Services;

public class TokenProvider : ITokenProvider
{
    private readonly IConfiguration _config;
    private readonly IDateTime _dateTime;

    public TokenProvider(IConfiguration config, IDateTime dateTime)
    {
        _config = config;
        _dateTime = dateTime;
    }

    public Token GenerateAccessToken(long userId, UserRoles role, long? companyId)
    {
        var secret = _config["Jwt:Secret"];
        var secretBytes = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var signinCredentials = new SigningCredentials(secretBytes, SecurityAlgorithms.HmacSha256);
        var expires = _dateTime.Now.AddMinutes(5);

        var claims = new List<Claim>
        {
            new("id", userId.ToString()), //
            new("role", role.ToString()),
        };

        if (companyId != null)
        {
            claims.Add(new Claim("companyId", companyId.Value.ToString()));
        }

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: expires,
            signingCredentials: signinCredentials
        );

        return new Token(new JwtSecurityTokenHandler().WriteToken(token), expires);
    }

    public Token GenerateRefreshToken(long userId)
    {
        var expires = _dateTime.UtcNow.AddDays(7);
        var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

        return new Token(token, expires);
    }

    public ClaimsPrincipal? GetPrincipalFromExpiredAccessToken(string? accessToken)
    {
        var secret = _config["Jwt:Secret"];
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _config["Jwt:Issuer"],
            ValidAudience = _config["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret))
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(
            accessToken, tokenValidationParameters, out SecurityToken securityToken);

        if (securityToken is not JwtSecurityToken jwtSecurityToken ||
            !jwtSecurityToken.Header.Alg
                .Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token");
        }

        return principal;
    }
}