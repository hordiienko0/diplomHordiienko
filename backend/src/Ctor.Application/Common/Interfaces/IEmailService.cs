using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ctor.Application.DTOs.EmailDTos;

namespace Ctor.Application.Common.Interfaces;
public interface IEmailService
{
    Task SendAsync(IEnumerable<EmailDTO> emails, string subject, string text, string html);
}
