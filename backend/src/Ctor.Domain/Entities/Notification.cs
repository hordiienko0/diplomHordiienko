using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ctor.Domain.Common;

namespace Ctor.Domain.Entities;
public class Notification : BaseEntity
{
    public long UserId { get;set; } // means to who belongs to
    public string Type { get;set; }
    public string Message { get;set; }
}
