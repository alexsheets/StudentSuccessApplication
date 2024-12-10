using System;
using System.Collections.Generic;

namespace EPA.Models;

public partial class Rotation
{
    public int RotationId { get; set; }

    public string Title { get; set; } = null!;

    public string? Abbreviation { get; set; } = null!;

    public string? Supervisors { get; set; }

    public bool? Remove { get; set; }

    public DateTime? UpdateDt { get; set; }
}
