using System;
using System.Collections.Generic;

namespace EPA.Models;

public partial class Student
{
    public long StudentId { get; set; }

    public string? PawsId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public long? ConcentrationId { get; set; }

    public DateTime? UpdateDt { get; set; }

    public long? ClassId { get; set; }

    public bool? Active { get; set; }
}
