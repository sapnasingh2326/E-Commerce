using System;
using System.Collections.Generic;

namespace ECommerceWeb.Model;

public partial class DynamicPage
{
    public int PageId { get; set; }

    public string? PageName { get; set; }

    public string? PageUrl { get; set; }

    public string? PageContent { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public bool? IsDeleted { get; set; }
}
