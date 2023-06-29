using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Ctor.Domain.Entities;
using Ctor.Domain.Repositories;

namespace Ctor.Infrastructure.Persistence.Repositories;
internal class PhaseStepRepository : GenericRepository<PhaseStep>, IPhaseStepRepository
{
    public PhaseStepRepository(ApplicationDbContext context, Lazy<IMapper> mapper) : base(context, mapper)
    {
    }
}
