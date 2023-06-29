using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Ctor.Application.Common.Mapping;
using Ctor.Domain.Entities;

namespace Ctor.Application.Projects.Commands.CreateProjectCommand;
public  class CreateProjectDTO: IMapFrom<Project>
{
    public int ProjectId { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateProjectDTO, Project>()
            .ForMember(destinationMember: x => x.ProjectType,res=>res.MapFrom(x=>String.Empty))
            .ForMember(x=>x.City, res=>res.MapFrom(x=> String.Empty))
            .ForMember(x=>x.Country, res=>res.MapFrom(x=> String.Empty))
            .ForMember(x=>x.ProjectName,res=>res.MapFrom(x=>x.Name))
            .ForMember(x=>x.EndTime,res=>res.MapFrom(x=>x.EndDate))
            .ForMember(x=>x.StartTime,res=>res.MapFrom(x=>x.StartDate));
           
    }
    
}
