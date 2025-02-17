using System;
using System.Collections.Generic;

namespace ECommerceWeb.Model;

public partial class SubCategory
{
    public int Id { get; set; }

    public string SubCategoryName { get; set; } = null!;

    public int? CategoryId { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
