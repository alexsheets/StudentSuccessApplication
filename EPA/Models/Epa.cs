using System;
using System.Collections.Generic;

namespace EPA.Models;

public partial class Epa
{
    public long Epaid { get; set; }

    public string? SectionTag { get; set; }

    public string? SectionName { get; set; }

    public DateTime? UpdateDt { get; set; }
}
