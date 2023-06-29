using AutoMapper;
using Ctor.Application.Common.Exceptions;
using Ctor.Domain.Entities;
using Ctor.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ctor.Infrastructure.Persistence.Repositories; 

public class UserRepository : GenericRepository<User>, IUserRepository
{

    public UserRepository(ApplicationDbContext context, Lazy<IMapper> mapper) : base(context, mapper)
    {
    }

    public Task<User?> FindByEmailAsync(string email, CancellationToken ct)
    {
        return table.FirstOrDefaultAsync(x => x.UserEmail == email, ct);
    }

    public async Task<User> GetByEmailAsync(string email, CancellationToken ct)
    {
        var user = await table
            .Include(u => u.Role) // todo: refactor from here
            .FirstOrDefaultAsync(x => x.UserEmail == email, ct);

        if (user == null)
        {
            throw new NotFoundException($"Entity \"{nameof(User)}\" was not found.");
        }

        return user;
    }

    public new async Task<User> GetById(long id, CancellationToken ct)
    {
        var entity = await FindById(id, ct);

        if (entity == null)
        {
            throw new NotFoundException(nameof(User), id);
        }

        return entity;
    }

    public new Task<User?> FindById(long id, CancellationToken ct)
    {
        return table
            .Include(x => x.Role) // todo: refactor from here
            .FirstOrDefaultAsync(e => e.Id == id, ct);
    }
}
