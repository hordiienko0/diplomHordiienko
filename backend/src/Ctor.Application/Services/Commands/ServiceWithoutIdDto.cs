using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ctor.Application.Services.Commands;
public class ServiceWithoutIdDto
{
    public string[] Types { get; set; }
    public string Company { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Website { get; set; }
}
