using System;
using System.Collections.Generic;

namespace ECommerceWeb.Model;

public partial class EmailTemp
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Subject { get; set; }

    public string? EmailContent { get; set; }

    public string? EmailUrl { get; set; }

    public bool? IsDeleted { get; set; }
}
