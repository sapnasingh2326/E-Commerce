using System;
using System.Collections.Generic;

namespace ECommerceWeb.Model;

public partial class Category
{
    public int Id { get; set; }

    public string CategoryName { get; set; } = null!;

    public string CategoryImageUrl { get; set; } = null!;

    public bool? IsDeleted { get; set; }

    public virtual ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
}
