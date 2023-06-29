using AutoMapper;
using Ctor.Application.Common.Exceptions;
using Ctor.Domain.Entities;
using Ctor.Domain.Entities.Enums;
using Ctor.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ctor.Infrastructure.Persistence.Repositories;

public class ProjectRepository : GenericRepository<Project>, IProjectRepository
{
    public ProjectRepository(ApplicationDbContext _context, Lazy<IMapper> mapper) : base(_context, mapper)
    {
    }

    public async Task SetTeamAsync(long projectId, ISet<long> userIds, long editingUserId)
    {
        var users = await _context.Set<Company>()
            .Where(c => c.Projects.Any(p => p.Id == projectId))
            .Where(c => c.Users
                .Any(u => u.Id == editingUserId
                          && (u.Role.Type == UserRoles.OperationalManager || u.Role.Type == UserRoles.ProjectManager)))
            .Take(1)
            .SelectMany(c => c.Users)
            .ToArrayAsync();

        if (users == null || users.Length == 0)
        {
            throw new NotFoundException();
        }

        var project = await _context.Set<Project>()
            .Include(p => p.Assignees)
            .SingleAsync(p => p.Id == projectId);

        project.Assignees.Clear();

        foreach (var user in users.Where(u => userIds.Contains(u.Id)))
        {
            project.Assignees.Add(new Assignee { User = user, Project = project });
        }
    }
}