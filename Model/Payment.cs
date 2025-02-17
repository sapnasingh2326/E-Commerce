using System;
using System.Collections.Generic;

namespace ECommerceWeb.Model;

public partial class Payment
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int OrderDetailId { get; set; }

    public string? TransactionId { get; set; }

    public string? Upiid { get; set; }

    public string? CardNo { get; set; }

    public string? ExpiryDate { get; set; }

    public int? CvvNo { get; set; }

    public string? Address { get; set; }

    public string? PaymentMode { get; set; }

    public DateTime PaymentDate { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual OrderList Order { get; set; } = null!;

    public virtual OrderDetail OrderDetail { get; set; } = null!;
}
