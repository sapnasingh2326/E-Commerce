using System;
using System.Collections.Generic;

namespace ECommerceWeb.Model;

public partial class PaymentMethod
{
    public int Id { get; set; }

    public string? MethodName { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<OrderList> OrderLists { get; set; } = new List<OrderList>();
}
