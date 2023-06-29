using Ctor.Application.Common.Interfaces;
using Ctor.Domain.Entities;
using Ctor.Domain.Entities.Enums;

namespace Ctor.Infrastructure.Services;

/*
 *  When user connects to NotificationHub, it is immediately added to special group
 *  Groups naming
 *  - admin             group for admins
 *  - {CompanyId}_OM    group for operational managers who belongs to
 *  company with id CompanyId
 *      *Example: 3_OM
 *  - {ProjectId}_PM    group for project managers who belong to specific 
 *  projects with id = ProjectId
 *  
 */


public class GroupsService : IGroupsService
{
    private readonly IApplicationDbContext _context;

    public GroupsService(IApplicationDbContext context)
    {
        this._context = context;
    }
    public async Task<List<string>> GetGroupsOfUserAsync(long? id)
    {
        List<string> roles = new List<string>();
        if (!id.HasValue) return roles;
        User? user = await _context.Users.FindById(id.Value);
        if(user == null) return roles;

        if (user.RoleId == 1) roles.Add("admin");
        if(user.RoleId == 2) { // Operational Manager
            roles.Add(user.CompanyId + "_OM");
        }
        if(user.RoleId == 3) // Project Manager
        {
            List<Project>? projects = await _context.Projects.Get(el => el.UserId == id);
            foreach (Project project in projects)
            {
                roles.Add(project.Id + "_PM");
            }
        }
        if(user.RoleId == 4) // Main engeneer
        {
            // !TODO Thereisn`t connection btw mi and project
        }
        return roles;
    }

    public async Task<List<long>> GetUsersFromGroup(string group)
    {
        List<long> UsersId = new List<long>();
        try
        {
            if (group == "admin")
            {
                var admins = await _context.Users.Get(user => user.RoleId == 1);
                foreach (User admin in admins)
                {
                    UsersId.Add(admin.Id);
                }
            }
            string[] partsOfname = group.Split("_");
            if (partsOfname.Length != 2) return UsersId;
            if (partsOfname[1] == "OM")
            {
                long CompanyId = (long)Convert.ToDouble(partsOfname[0]);
                var managers = await _context.Users.Get(user => user.CompanyId == CompanyId && user.RoleId == 2);
                if (managers != null) foreach (User user in managers) { UsersId.Add(user.Id); }
            }
            if(partsOfname[1] == "PM")
            {
                long ProjectId = (long)Convert.ToDouble(partsOfname[0]);
                var projects = await _context.Projects.Get(el => el.Id == ProjectId);
                if (projects != null) foreach (Project project in projects) {
                    if(project.UserId.HasValue) UsersId.Add(project.UserId.Value);
                }
            }
        }
        catch { }
        return UsersId;
    }
}
