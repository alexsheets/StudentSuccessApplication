using System;
using System.Collections.Generic;

namespace EPA.Models;

public partial class Concentration
{
    public long ConcentrationId { get; set; }

    public string? Description { get; set; }

    public DateTime? UpdateDt { get; set; }
}
