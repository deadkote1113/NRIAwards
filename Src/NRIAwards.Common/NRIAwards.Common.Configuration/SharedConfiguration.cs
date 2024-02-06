using NRIAwards.Common.Configuration.Mail;

namespace NRIAwards.Common.Configuration;

public class SharedConfiguration
{
    public static string DbConnectionString { get; private set; }

    public static SmtpConfiguration SmtpConfiguration { get; private set; }


    public static void UpdateSharedConfiguration(string dbConnectionString, SmtpConfiguration smtpConfiguration)
    {
        DbConnectionString = dbConnectionString;
        SmtpConfiguration = smtpConfiguration;
    }
}
