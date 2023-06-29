using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ctor.Application.Common.Interfaces;
public interface ISecurityService
{
    string ComputeSha256Hash(string rawData);
}
