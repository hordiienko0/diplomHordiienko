using AutoMapper;
using Ctor.Domain.Entities;
using Ctor.Domain.Repositories;

namespace Ctor.Infrastructure.Persistence.Repositories;

public class MeasurementRepository : GenericRepository<Measurement>, IMeasurementRepository
{
    public MeasurementRepository(ApplicationDbContext context, Lazy<IMapper> mapper) : base(context, mapper)
    {
    }
}