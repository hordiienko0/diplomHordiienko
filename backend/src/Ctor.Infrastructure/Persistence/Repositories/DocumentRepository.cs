using AutoMapper;
using Ctor.Domain.Entities;
using Ctor.Domain.Repositories;

namespace Ctor.Infrastructure.Persistence.Repositories;

public class DocumentRepository : GenericRepository<Document>, IDocumentRepository
{
    public DocumentRepository(ApplicationDbContext context, Lazy<IMapper> mapper) : base(context, mapper)
    {
    }
}