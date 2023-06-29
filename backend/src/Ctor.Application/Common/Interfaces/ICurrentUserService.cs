using Ctor.Domain.Entities.Enums;

namespace Ctor.Application.Common.Interfaces;

public interface ICurrentUserService
{
    long? Id { get; }
    UserRoles Role { get; }
    long? CompanyId { get; }
}