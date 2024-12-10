using System;
using System.Collections.Generic;

namespace EPA.Models;

public partial class ResultComment
{
    public long ResultsCommentId { get; set; }

    public long? ResultId { get; set; }

    public string? Comment1 { get; set; }

    public string? Comment2 { get; set; }

    public DateTime? UpdateDt { get; set; }
}
