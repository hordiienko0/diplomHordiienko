using AutoMapper;
using Ctor.Domain.Entities;
using Ctor.Domain.Repositories;

namespace Ctor.Infrastructure.Persistence.Repositories;
public class MaterialRepository : GenericRepository<Material>, IMaterialRepository
{
    public MaterialRepository(ApplicationDbContext context, Lazy<IMapper> mapper) : base(context, mapper)
    {
    }
}