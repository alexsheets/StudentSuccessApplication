using System;
using System.Collections.Generic;

namespace EPA.Models;

public partial class EpatoCompetency
{
    public long EpacompId { get; set; }

    public long? CompetencyId { get; set; }

    public long? Epaid { get; set; }

    public DateTime? UpdateDt { get; set; }
}
