using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ctor.Application.Common.Enums;

namespace Ctor.Application.Common.Models;
public class SortModel
{
    public string Sort { get; set; }
    public Order Order { get; set; } = Order.ASC;
}
