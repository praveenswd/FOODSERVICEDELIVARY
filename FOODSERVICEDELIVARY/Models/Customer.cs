using System;
using System.Collections.Generic;

namespace FOODSERVICEDELIVARY.Models;

public partial class Customer
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Tip> Tips { get; set; } = new List<Tip>();
}
