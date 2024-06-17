using System;
using System.Collections.Generic;

namespace FOODSERVICEDELIVARY.Models;

public partial class Driver
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
