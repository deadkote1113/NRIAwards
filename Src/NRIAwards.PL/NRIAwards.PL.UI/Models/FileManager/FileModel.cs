﻿namespace NRIAwards.PL.UI.Models.FileManager;

public class FileModel
{
    public string Name { get; set; }

    public long Size { get; set; }

    public DateTime LastModified { get; set; }

    public string RelativeUrl { get; set; }
}