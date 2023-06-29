using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ctor.Application.DTOs;
public class BlobResponseDTO
{
    public string Status { get; set; }
    public bool Error { get; set; }
    public string Uri { get; set; }
    public string Name { get; set; }
}
