﻿using NRIAwards.Common.Configuration.Mail;

namespace NRIAwards.Common.Configuration;

public class SharedConfiguration
{
	public const int VisualContentPlaceholderId = 1;

    public required SmtpConfiguration SmtpConfiguration { get; init; }
}
