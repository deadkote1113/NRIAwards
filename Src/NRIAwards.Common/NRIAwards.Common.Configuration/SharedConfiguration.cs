using NRIAwards.Common.Configuration.Mail;

namespace NRIAwards.Common.Configuration;

public class SharedConfiguration
{
    public required string DbConnectionString { get; init; }

    public required SmtpConfiguration SmtpConfiguration { get; init; }

}
