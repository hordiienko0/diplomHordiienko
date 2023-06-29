using Ctor.Application.Common.Exceptions;
using Ctor.Application.Common.Interfaces;
using Ctor.Application.Common.Models;
using Ctor.Application.DTOs.EmailDTos;
using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace Ctor.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly MailSetting _mailSetting;
    public EmailService(IOptions<MailSetting> mailSetting)
    {
        _mailSetting = mailSetting.Value;
    }


    public async Task SendAsync(IEnumerable<EmailDTO> emails, string subject, string text, string html)
    {
        if (_mailSetting == null)
            throw new EmailException("Email settings not found");


        MailjetClient client = new MailjetClient(
            _mailSetting.ApiKey,
            _mailSetting.ApiSecret
            );

        var request = GetMailjetRequest(emails, subject, text, html);

        MailjetResponse response = await client.PostAsync(request);

        if (!response.IsSuccessStatusCode)
            throw new EmailException("Message couldn't be sent");
    }
    public async Task SendAsync(IEnumerable<EmailDTO> emails, string subject, string html)
    {
        if (_mailSetting == null)
            throw new EmailException("Email settings not found");


        MailjetClient client = new MailjetClient(
            _mailSetting.ApiKey,
            _mailSetting.ApiSecret
            );

        var request = GetMailjetRequest(emails, subject, html);

        MailjetResponse response = await client.PostAsync(request);

        if (!response.IsSuccessStatusCode)
            throw new EmailException("Message couldn't be sent");
    }

    private MailjetRequest GetMailjetRequest(IEnumerable<EmailDTO> emails, string subject, string html)
    {
        var from = new JObject
        {
             {"Email", _mailSetting.FromEmail},
             {"Name", _mailSetting.DiplayName}
        };

        var to = new JArray();
        var bodyRequest = new JArray();
        foreach (var email in emails)
        {
            var emailJObject = new JObject {

                {"Email", email.Email},
                {"Name",email.Name}
              };
            to.Add(emailJObject);
            var bodyJObject = new JObject {

               {"From", from},
               {"To",to},
               {"Subject",subject},
               {"TextPart",email.Text},
               {"HTMLPart",html},
               {"CustomID","AppGettingStartedTest"}

           };
            bodyRequest.Add(bodyJObject);
        }

        MailjetRequest request = new MailjetRequest
        {
            Resource = SendV31.Resource,
        }
        .Property(Send.Messages, bodyRequest);

        return request;
    }

    private MailjetRequest GetMailjetRequest(IEnumerable<EmailDTO> emails, string subject, string text, string html)
    {
        var from = new JObject
        {
             {"Email", _mailSetting.FromEmail},
             {"Name", _mailSetting.DiplayName}
        };

        var to = new JArray();
        foreach (var email in emails)
        {
            var emailJObject = new JObject {

                {"Email", email.Email},
                {"Name",email.Name}
              };
            to.Add(emailJObject);
        }

        var bodyRequest = new JArray {
           new JObject {

               {"From", from},
               {"To",to},
               {"Subject",subject},
               {"TextPart",text},
               {"HTMLPart",html},
               {"CustomID","AppGettingStartedTest"}

           }
      };

        MailjetRequest request = new MailjetRequest
        {
            Resource = SendV31.Resource,
        }
        .Property(Send.Messages, bodyRequest);

        return request;
    }
}
