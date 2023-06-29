using AutoMapper;
using Ctor.Domain.Entities;
using Ctor.Domain.Repositories;

namespace Ctor.Infrastructure.Persistence.Repositories;

public class CompanyLogoRepository: GenericRepository<CompanyLogo>, ICompanyLogoRepository
{
    public CompanyLogoRepository(ApplicationDbContext context, Lazy<IMapper> mapper) : base(context, mapper)
    {
    }
}