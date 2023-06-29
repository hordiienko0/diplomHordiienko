using AutoMapper;
using Ctor.Domain.Entities;
using Ctor.Domain.Repositories;

namespace Ctor.Infrastructure.Persistence.Repositories;

public class AssigneeRepository : GenericRepository<Assignee>, IAssigneeRepository
{
    public AssigneeRepository(ApplicationDbContext _context, Lazy<IMapper> mapper) : base(_context, mapper) { }
}