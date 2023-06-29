using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ctor.Application.Common.Models;
public class PaginationModel<T>
{
    public List<T> List { get; set; }
    public int Total { get; set; }
}
