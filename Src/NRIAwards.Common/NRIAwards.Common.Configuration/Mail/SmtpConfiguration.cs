namespace NRIAwards.Common.Configuration.Mail;

public class SmtpConfiguration
{
    public required string Host { get; init; }
    public required int Port { get; init; }
    public required bool UseSsl { get; init; }
    public required string UserName { get; init; }
    public required string UserPassword { get; init; }
}
