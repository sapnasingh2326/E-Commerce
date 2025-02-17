using System;
using System.Collections.Generic;

namespace ECommerceWeb.Model;

public partial class Brand
{
    public int Id { get; set; }

    public string? BrandName { get; set; }

    public string? BrandLogo { get; set; }

    public bool? IsDeleted { get; set; }
}
