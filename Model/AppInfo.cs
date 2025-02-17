using System;
using System.Collections.Generic;

namespace ECommerceWeb.Model;

public partial class AppInfo
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? FavIcon { get; set; }

    public string? Logo { get; set; }

    public string? LogoBackGroundImage { get; set; }

    public bool? IsDeleted { get; set; }
}
