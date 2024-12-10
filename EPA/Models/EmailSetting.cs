using System;
using System.Collections.Generic;

namespace EPA.Models;

public partial class EmailSetting
{
    public int EmailSettingsId { get; set; }

    public string FromEmail { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Smtpclient { get; set; } = null!;

    public int PortNumber { get; set; }
}
