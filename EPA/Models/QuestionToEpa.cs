using System;
using System.Collections.Generic;

namespace EPA.Models;

public partial class QuestionToEpa
{
    public long QuestionToEpasId { get; set; }

    public long? QuestionId { get; set; }

    public long? Epaid { get; set; }

    public DateTime? UpdateDt { get; set; }
}
