using System;
using System.Collections.Generic;

namespace EPA.Models;

public partial class Competency
{
    public long CompetencyId { get; set; }

    public string? Identifier { get; set; }

    public string? Description { get; set; }

    public DateTime? UpdateDt { get; set; }
}
