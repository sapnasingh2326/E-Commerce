using System;
using System.Collections.Generic;

namespace ECommerceWeb.Model;

public partial class Banner
{
    public int Id { get; set; }

    public string? BannerName { get; set; }

    public string? BannerImg { get; set; }

    public string? BannerHtmlContent { get; set; }

    public bool? IsDeleted { get; set; }
}
