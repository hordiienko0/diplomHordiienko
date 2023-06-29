using AutoMapper;
using Ctor.Domain.Entities;
using Ctor.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ctor.Infrastructure.Persistence.Repositories;
internal class RoleRepository : GenericRepository<Role>, IRoleRepository
{
    public RoleRepository(ApplicationDbContext _context, Lazy<IMapper> mapper) : base(_context, mapper)
    {

    }
}
