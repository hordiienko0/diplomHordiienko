using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ctor.Application.DTOs;
public class PaginationQueryModelDTO : QueryModelDTO
{
    public int Page { get; set; }
    public int Count { get; set; }
}
