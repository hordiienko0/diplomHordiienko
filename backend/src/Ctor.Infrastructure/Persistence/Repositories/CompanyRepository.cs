using AutoMapper;
using Ctor.Domain.Entities;
using Ctor.Domain.Repositories;
namespace Ctor.Infrastructure.Persistence.Repositories;
public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
{
    public CompanyRepository(ApplicationDbContext _context, Lazy<IMapper> mapper) : base(_context, mapper) {}
}
