using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ctor.Application.Users.Commands;
using Ctor.Domain.Common;
using Ctor.Domain.Entities;

namespace Ctor.Application.Common.Interfaces;
public interface ICsvFileService
{
    public List<string> ErrorLines { get; }
    public List<UserCsvDto> ReadMembers(Stream fileStream);
}
