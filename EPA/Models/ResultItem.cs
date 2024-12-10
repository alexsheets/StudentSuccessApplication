using System;
using System.Collections.Generic;

namespace EPA.Models;

public partial class ResultItem
{
    public long ResultsItemId { get; set; }

    public long? ResultId { get; set; }

    public long? QuestionId { get; set; }

    public double? Score { get; set; }

    public string? SelfEvalReflection1 { get; set; }

    public string? SelfEvalReflection2 { get; set; }

    public string? SelfEvalReflection3 { get; set; }

    public DateTime? UpdateDt { get; set; }
}
