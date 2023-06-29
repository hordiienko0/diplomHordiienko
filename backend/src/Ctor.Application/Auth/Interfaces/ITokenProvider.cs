using System.Security.Claims;
using Ctor.Application.Auth.Models;
using Ctor.Domain.Entities.Enums;

namespace Ctor.Application.Auth.Interfaces;

public interface ITokenProvider
{
    Token GenerateAccessToken(long userId, UserRoles role, long? companyId);

    Token GenerateRefreshToken(long userId);

    ClaimsPrincipal? GetPrincipalFromExpiredAccessToken(string? accessToken);
}