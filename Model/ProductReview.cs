using System;
using System.Collections.Generic;

namespace ECommerceWeb.Model;

public partial class ProductReview
{
    public int Id { get; set; }

    public int Rating { get; set; }

    public string? Comment { get; set; }

    public int ProductId { get; set; }

    public int RegistrationId { get; set; }

    public DateTime CreatedDate { get; set; }

    public string? ImageOne { get; set; }

    public string? ImageTwo { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual Registration Registration { get; set; } = null!;
}
