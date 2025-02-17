using System;
using System.Collections.Generic;

namespace ECommerceWeb.Model;

public partial class State
{
    public int Id { get; set; }

    public string? StateName { get; set; }

    public int? CountryId { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<City> Cities { get; set; } = new List<City>();

    public virtual Country? Country { get; set; }
}
