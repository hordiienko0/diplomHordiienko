using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ctor.Application.Common.Models;

namespace Ctor.Application.Common.Interfaces;
public interface IAddressParsingService
{
    AddressParsedModel ParseAddress(string address);
}
