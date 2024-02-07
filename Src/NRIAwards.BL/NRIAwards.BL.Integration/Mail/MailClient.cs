using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using NRIAwards.Common.Configuration;
using NRIAwards.Common.Configuration.Mail;

namespace NRIAwards.BL.Integration.Mail;

public class MailClient : IMailClient
{
    private readonly SmtpConfiguration smtpConfiguration;

    public MailClient(SharedConfiguration sharedConfiguration)
    {
        smtpConfiguration = sharedConfiguration.SmtpConfiguration;
    }

    public async Task SendAsync(SmtpClient client, string senderName,
        IEnumerable<string> recipients, string subject, string body, bool sendBlindCopies = true)
    {
        await client.ConnectAsync(smtpConfiguration.Host, smtpConfiguration.Port, smtpConfiguration.UseSsl);
        await client.AuthenticateAsync(smtpConfiguration.UserName, smtpConfiguration.UserPassword);
        var message = new MimeMessage
        {
            Subject = subject,
            Body = new TextPart(TextFormat.Html)
            {
                Text = body
            }
        };
        message.From.Add(new MailboxAddress(senderName, smtpConfiguration.UserName));
        recipients = recipients.ToList();
        if (sendBlindCopies && recipients.Count() > 1)
        {
            message.To.Add(new MailboxAddress(null, recipients.First()));
            message.Bcc.AddRange(recipients.Select(item => new MailboxAddress(null, item)).Skip(1));
        }
        else
        {
            message.To.AddRange(recipients.Select(item => new MailboxAddress(null, item)));
        }
        await client.SendAsync(message);
    }

    public async Task SendAsync(SmtpClient client, string senderName, string recipients, string subject, string body, bool sendBlindCopies = true)
    {
        await SendAsync(
            client,
            senderName,
            recipients.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(item => item.Trim()),
            subject,
            body,
            sendBlindCopies);
    }
}
