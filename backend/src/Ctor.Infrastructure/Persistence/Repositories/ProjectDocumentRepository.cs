using AutoMapper;
using Ctor.Domain.Entities;
using Ctor.Domain.Repositories;

namespace Ctor.Infrastructure.Persistence.Repositories;

public class ProjectDocumentRepository : GenericRepository<ProjectDocument>, IProjectDocumentRepository
{
    public ProjectDocumentRepository(ApplicationDbContext context, Lazy<IMapper> mapper) : base(context, mapper)
    {
    }
}