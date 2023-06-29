using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Ctor.Application.Common.Mapping;
using Ctor.Domain.Entities;

namespace Ctor.Application.Phases.Commands.UpdatePhaseSteps;
public class PhaseStepPutDto : IMapFrom<PhaseStep>
{
    public long? Id { get; set; }
    public string PhaseStepName { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsDone { get; set; }
    public long? BuildingId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<PhaseStep, PhaseStepPutDto>().ReverseMap();
    }
}
