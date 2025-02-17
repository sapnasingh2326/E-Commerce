using System;
using System.Collections.Generic;

namespace ECommerceWeb.Model;

public partial class Product
{
    public int Id { get; set; }

    public int SubCategoryId { get; set; }

    public string ProductCode { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public string? ShortDescription { get; set; }

    public string? LongDescription { get; set; }

    public decimal Price { get; set; }

    public int Quality { get; set; }

    public int ProductSizeId { get; set; }

    public int ProductColorId { get; set; }

    public string? BrandName { get; set; }

    public int? AvailableQty { get; set; }

    public bool IsNewarrivals { get; set; }

    public bool IsThreeitems { get; set; }

    public bool IsTwoitems { get; set; }

    public bool IsAvailable { get; set; }

    public bool IsStock { get; set; }

    public DateTime? CreatedDate { get; set; }

    public bool? IsDeleted { get; set; }

    public string? ProductDisplayImage { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ProductColor ProductColor { get; set; } = null!;

    public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();

    public virtual ICollection<ProductReview> ProductReviews { get; set; } = new List<ProductReview>();

    public virtual ProductSize ProductSize { get; set; } = null!;

    public virtual SubCategory SubCategory { get; set; } = null!;

    public virtual ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();
}
