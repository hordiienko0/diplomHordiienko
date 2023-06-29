using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ctor.Application.Common.Models;

namespace Ctor.Application.DTOs;
public class QueryModelDTO : SortModel
{
    [DisplayFormat(ConvertEmptyStringToNull = false)]
    public string Query { get; set; } = String.Empty;
}
