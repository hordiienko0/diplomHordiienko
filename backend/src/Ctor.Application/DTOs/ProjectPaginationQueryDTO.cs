using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ctor.Domain.Entities.Enums;

namespace Ctor.Application.DTOs;
public class ProjectPaginationQueryDTO : PaginationQueryModelDTO
{
    public ProjectStatus Status { get; set; }
}
