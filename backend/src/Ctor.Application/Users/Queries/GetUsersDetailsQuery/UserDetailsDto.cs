using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Ctor.Application.Common.Mapping;
using Ctor.Domain.Entities;

namespace Ctor.Application.Users.Queries.GetUsersDetailsQuery;
public class UserDetailsDto : IMapFrom<User>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserEmail { get; set; }
    public long CompanyId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<User, UserDetailsDto>();
           
    }
}
