using System;
using System.Collections.Generic;

namespace ECommerceWeb.Model;

public partial class ProductImage
{
    public int Id { get; set; }

    public string ImageUrl { get; set; } = null!;

    public int Product { get; set; }

    public bool? DefaultImage { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Product ProductNavigation { get; set; } = null!;
}
