using Ctor.Domain.Entities;

namespace Ctor.Domain.Repositories;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User?> FindByEmailAsync(string email, CancellationToken ct);

    Task<User> GetByEmailAsync(string email, CancellationToken ct);

    new Task<User> GetById(long id, CancellationToken ct); // todo: remove (see comment in the implementation)

    new Task<User?> FindById(long id, CancellationToken ct); // todo: remove (see comment in the implementation)

}
