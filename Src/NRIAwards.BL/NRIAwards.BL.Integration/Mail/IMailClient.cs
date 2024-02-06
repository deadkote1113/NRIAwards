using MailKit.Net.Smtp;

namespace NRIAwards.BL.Integration.Mail;

public interface IMailClient
{
    Task SendAsync(SmtpClient client, string senderName,
        IEnumerable<string> recipients, string subject, string body, bool sendBlindCopies = true);

    Task SendAsync(SmtpClient client, string senderName, string recipients, string subject, string body, bool sendBlindCopies = true);
}