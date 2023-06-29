using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ctor.Domain.Common;

namespace Ctor.Domain.Entities;
public class VendorType: BaseEntity
{
    public string Name { get; set; }

    public virtual ICollection<Vendor> Vendors { get; set; }
}
 