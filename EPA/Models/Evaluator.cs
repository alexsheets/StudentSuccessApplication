using System;
using System.Collections.Generic;

namespace EPA.Models;

public partial class Evaluator
{
    public long EvaluatorId { get; set; }

    public string? Email { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Password { get; set; }

    public bool? Lsuind { get; set; }

    public string? Clinic { get; set; }

    public DateTime? UpdateDt { get; set; }

    public bool? Active { get; set; }
}
