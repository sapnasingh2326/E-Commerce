using System;
using System.Collections.Generic;

namespace ECommerceWeb.Model;

public partial class OrderDetail
{
    public int Id { get; set; }

    public int? OrderId { get; set; }

    public int? ProductId { get; set; }

    public int? ProductPrice { get; set; }

    public int? Quatity { get; set; }

    public int? FinalPrice { get; set; }

    public int? RegistrationId { get; set; }

    public string? Status { get; set; }

    public string? PaymentStatus { get; set; }

    public DateTime OrderDate { get; set; }

    public bool IsCancel { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual OrderList? Order { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Product? Product { get; set; }

    public virtual Registration? Registration { get; set; }
}
