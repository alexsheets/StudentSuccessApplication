using System;
using System.Collections.Generic;

namespace EPA.Models;

public partial class Question
{
    public int QuestionId { get; set; }


    public string QuestionText { get; set; } = null!;


    public bool? Remove { get; set; }
}
