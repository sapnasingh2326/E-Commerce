using System;
using System.Collections.Generic;

namespace ECommerceWeb.Model;

public partial class City
{
    public int Id { get; set; }

    public string? CityName { get; set; }

    public int? StateId { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual State? State { get; set; }
}
