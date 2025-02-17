using System;
using System.Collections.Generic;

namespace ECommerceWeb.Model;

public partial class ProductColor
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool? IsDeleted { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
