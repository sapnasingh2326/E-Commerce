using System;
using System.Collections.Generic;

namespace ECommerceWeb.Model;

public partial class Cart
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int? Quality { get; set; }

    public int RegistrationId { get; set; }

    public DateTime CreatedDate { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual Registration Registration { get; set; } = null!;
}
