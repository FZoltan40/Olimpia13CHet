using System;
using System.Collections.Generic;

namespace OlimpiaApi.Models;

public partial class Data
{
    public Guid Id { get; set; }

    public string? Country { get; set; }

    public string? County { get; set; }

    public string? Description { get; set; }

    public DateTime? CreatedTime { get; set; }

    public DateTime? UpdatedTime { get; set; }

    public Guid? PalyerId { get; set; }

    public virtual Player? Palyer { get; set; }
}
