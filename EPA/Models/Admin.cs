using System;
using System.Collections.Generic;

namespace EPA.Models;

public partial class Admin
{
    public long AdmintId { get; set; }

    public string PawsId { get; set; } = null!;

    public DateTime? UpdateDt { get; set; }
}
