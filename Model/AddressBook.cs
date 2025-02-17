using System;
using System.Collections.Generic;

namespace ECommerceWeb.Model;

public partial class AddressBook
{
    public int Id { get; set; }

    public int RegistrationId { get; set; }

    public string? FullName { get; set; }

    public string? AddressLine1 { get; set; }

    public string? AddressLine2 { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? Country { get; set; }

    public string? PostalCode { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<OrderList> OrderLists { get; set; } = new List<OrderList>();

    public virtual Registration Registration { get; set; } = null!;
}
