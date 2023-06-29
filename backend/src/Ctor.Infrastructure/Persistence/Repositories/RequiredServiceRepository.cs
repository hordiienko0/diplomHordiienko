using AutoMapper;
using Ctor.Domain.Entities;
using Ctor.Domain.Repositories;

namespace Ctor.Infrastructure.Persistence.Repositories;

public class RequiredServiceRepository : GenericRepository<RequiredService>, IRequiredServiceRepository
{
    public RequiredServiceRepository(ApplicationDbContext _context, Lazy<IMapper> mapper) : base(_context, mapper) { }
}