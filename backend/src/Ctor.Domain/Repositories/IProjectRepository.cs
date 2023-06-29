using Ctor.Domain.Entities;

namespace Ctor.Domain.Repositories;

public interface IProjectRepository : IGenericRepository<Project>
{
    Task SetTeamAsync(long projectId, ISet<long> userIds, long editingUserId);
}