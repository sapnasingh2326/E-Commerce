using System;
using System.Collections.Generic;

namespace ECommerceWeb.Model;

public partial class OrderItem
{
    public int Id { get; set; }

    public int? OrderId { get; set; }

    public int? ProductId { get; set; }

    public int? ItemQuantity { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Order? Order { get; set; }

    public virtual Product? Product { get; set; }
}
