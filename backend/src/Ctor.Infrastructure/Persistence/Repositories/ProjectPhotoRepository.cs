using AutoMapper;
using Ctor.Domain.Entities;
using Ctor.Domain.Repositories;

namespace Ctor.Infrastructure.Persistence.Repositories;

public class ProjectPhotoRepository : GenericRepository<ProjectPhoto>, IProjectPhotoRepository
{
    public ProjectPhotoRepository(ApplicationDbContext context, Lazy<IMapper> mapper) : base(context, mapper)
    {
    }
}