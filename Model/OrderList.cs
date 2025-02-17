using System;
using System.Collections.Generic;

namespace ECommerceWeb.Model;

public partial class OrderList
{
    public int Id { get; set; }

    public string? OrderNo { get; set; }

    public int? TotalQuatity { get; set; }

    public int? RegistrationId { get; set; }

    public int? AddressId { get; set; }

    public string? Status { get; set; }

    public string? PaymentStatus { get; set; }

    public DateTime OrderDate { get; set; }

    public int? SubTotal { get; set; }

    public int? Discount { get; set; }

    public int? PayablePrice { get; set; }

    public bool IsCancel { get; set; }

    public int? PaymentMethodId { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual AddressBook? Address { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual PaymentMethod? PaymentMethod { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Registration? Registration { get; set; }
}
