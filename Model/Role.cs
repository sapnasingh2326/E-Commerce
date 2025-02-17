using System;
using System.Collections.Generic;

namespace ECommerceWeb.Model;

public partial class Role
{
    public int Id { get; set; }

    public string RoleName { get; set; } = null!;

    public bool? IsDeleted { get; set; }

    public virtual ICollection<Registration> Registrations { get; set; } = new List<Registration>();
}
