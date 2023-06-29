using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ctor.Application.Common.Exceptions;
using Ctor.Application.Common.Interfaces;
using Ctor.Application.Common.Models;

namespace Ctor.Infrastructure.Services;
public class AddressParsingService : IAddressParsingService
{
    public AddressParsedModel ParseAddress(string address)
    {
        var pieceOfAddress = address.Split(',', StringSplitOptions.RemoveEmptyEntries);
        if (pieceOfAddress.Length != 3)
        {
            throw new BadAddressProvidedException();
        }

        return new AddressParsedModel
        {
            Country = pieceOfAddress[0].Trim(),
            City = pieceOfAddress[1].Trim(),
            Address = pieceOfAddress[2].Trim(),
        };
    }
}
