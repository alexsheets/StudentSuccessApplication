using System;
using System.Collections.Generic;

namespace EPA.Models;

public partial class Block
{
    public long BlockId { get; set; }

    public string Title { get; set; } = null!;

    public int? RotationId { get; set; }

    public bool? Remove { get; set; }

    public DateTime? UpdateDt { get; set; }
}
