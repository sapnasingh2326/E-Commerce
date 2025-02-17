using System;
using System.Collections.Generic;

namespace ECommerceWeb.Model;

public partial class ContactU
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Subject { get; set; }

    public string? Message { get; set; }

    public DateTime CreatedDate { get; set; }

    public bool? IsResolved { get; set; }

    public bool? IsDeleted { get; set; }
}
