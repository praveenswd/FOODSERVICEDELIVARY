using System;
using System.Collections.Generic;

namespace FOODSERVICEDELIVARY.Models;

public partial class Tip
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public decimal Amount { get; set; }

    public DateTime TipTime { get; set; }

    public virtual Customer Customer { get; set; } = null!;
}
