using System;
using System.Collections.Generic;

namespace FOODSERVICEDELIVARY.Models;

public partial class Order
{
    public int Id { get; set; }

    public DateTime OrderTime { get; set; }

    public int RestaurantId { get; set; }

    public int CustomerId { get; set; }

    public bool IsTipped { get; set; }

    public decimal TipAmount { get; set; }

    public string PickupLocation { get; set; } = null!;

    public string DeliveryLocation { get; set; } = null!;

    public int? DriverId { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Driver? Driver { get; set; }

    public virtual Restaurant Restaurant { get; set; } = null!;
}
