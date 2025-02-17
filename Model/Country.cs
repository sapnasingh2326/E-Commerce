using System;
using System.Collections.Generic;

namespace ECommerceWeb.Model;

public partial class Country
{
    public int Id { get; set; }

    public string? CountryName { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<State> States { get; set; } = new List<State>();
}
