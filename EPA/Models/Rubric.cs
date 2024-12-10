using System;
using System.Collections.Generic;

namespace EPA.Models;

public partial class Rubric
{
    public long RubricId { get; set; }

    public string Text { get; set; } = null!;

    public string? Description { get; set; }

    public bool? Remove { get; set; }

    public double? Score { get; set; }

    public DateTime? UpdateDt { get; set; }
}
