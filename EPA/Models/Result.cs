using EPA.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EPA.Models;

public partial class Result
{
    public long ResultsId { get; set; }

    public long Epaid { get; set; }

    [Required]
    public long StudentId { get; set; }

    public double? AgScore { get; set; }

    public DateTime? DateOfEval { get; set; }

    [Required]
    public long EvaluatorId { get; set; }

    [Required]
    public long RotationId { get; set; }

    public long? BlockId { get; set; }

    public string? Semester { get; set; }

    public bool? SelfEval { get; set; }

    public DateTime? UpdateDt { get; set; }

    public bool? Visibility { get; set; }

    public string? RequestToEvaluator { get; set; }
}
