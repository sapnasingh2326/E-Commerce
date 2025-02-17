using System;
using System.Collections.Generic;

namespace ECommerceWeb.Model;

public partial class NewsletterSubscription
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string? Email { get; set; }

    public DateOnly? SubscribeDate { get; set; }

    public bool? IsDeleted { get; set; }
}
