using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ctor.Application.Companies.Commands;
public class NewCompanyDto
{
    [Required]
    [Range(0.0, long.MaxValue, ErrorMessage = "CompanyId must be greater then 0.")]
    public long CompanyId { get; set; }
    [Required]
    public string CompanyName { get; set; }
    [Required]
    public string Country { get; set; }
    [Required]
    public string City { get; set; }
    [Required]
    public string Address { get; set; }
    [Required]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; }
}
