using System;
using System.Collections.Generic;

namespace ECommerceWeb.Model;

public partial class InvoicePage
{
    public int InvoiceId { get; set; }

    public DateOnly InvoiceDate { get; set; }

    public decimal? UnitPrice { get; set; }

    public decimal? TotalPrice { get; set; }
}
