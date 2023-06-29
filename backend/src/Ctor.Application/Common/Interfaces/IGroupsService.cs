using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ctor.Domain.Entities.Enums;

namespace Ctor.Application.Common.Interfaces;
public interface IGroupsService
{
    Task<List<string>> GetGroupsOfUserAsync(long? id);

    Task<List<long>> GetUsersFromGroup(string group);
}
