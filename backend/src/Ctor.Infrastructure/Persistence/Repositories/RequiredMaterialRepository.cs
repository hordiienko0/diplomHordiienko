using AutoMapper;
using Ctor.Domain.Entities;
using Ctor.Domain.Repositories;

namespace Ctor.Infrastructure.Persistence.Repositories;

public class RequiredMaterialRepository : GenericRepository<RequiredMaterial>, IRequiredMaterialRepository
{
    public RequiredMaterialRepository(ApplicationDbContext context, Lazy<IMapper> mapper) : base(context, mapper)
    {
    }
}