using System;
using System.Collections.Generic;

namespace ECommerceWeb.Model;

public partial class Registration
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? UserName { get; set; }

    public string? Mobile { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public string? PostCode { get; set; }

    public string? Password { get; set; }

    public string? ImageUrl { get; set; }

    public DateTime CreatedDate { get; set; }

    public bool? IsDeleted { get; set; }

    public int? RoleId { get; set; }

    public virtual ICollection<AddressBook> AddressBooks { get; set; } = new List<AddressBook>();

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<OrderList> OrderLists { get; set; } = new List<OrderList>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<ProductReview> ProductReviews { get; set; } = new List<ProductReview>();

    public virtual Role? Role { get; set; }

    public virtual ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();
}
