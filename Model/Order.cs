using System;
using System.Collections.Generic;

namespace ECommerceWeb.Model;

public partial class Order
{
    public int Id { get; set; }

    public int? CustomerId { get; set; }

    public string? OrderNumber { get; set; }

    public DateTime? OrderDate { get; set; }

    public int? OrderStatus { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Registration? Customer { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
