using System;
using System.Security.Claims;
using Ctor.Application.Common.Interfaces;
using Ctor.Domain.Entities.Enums;

namespace Ctor.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public long? Id
    {
        get
        {
            var claim = _httpContextAccessor.HttpContext?.User?.FindFirstValue("id");

            if (long.TryParse(claim, out long id))
            {
                return id;
            }

            return null;
        }
    }

    public UserRoles Role
    {
        get
        {
            var role = _httpContextAccessor.HttpContext?.User
                ?.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType)?.Value;
            var roleEnum = Enum.TryParse(role, out UserRoles result);

            if (!roleEnum)
            {
                throw new ArgumentException("bad roles");
            }

            return result;
        }
    }

    public long? CompanyId
    {
        get
        {
            var claim = _httpContextAccessor.HttpContext?.User?.FindFirstValue("companyId");

            if (long.TryParse(claim, out long id))
            {
                return id;
            }

            return null;
        }
    }
}