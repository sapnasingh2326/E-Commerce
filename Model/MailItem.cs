using System;
using System.Collections.Generic;

namespace ECommerceWeb.Model;

public partial class MailItem
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Sender { get; set; } = null!;

    public string Subject { get; set; } = null!;

    public string Body { get; set; } = null!;

    public DateTime ReceivedDate { get; set; }

    public string? MailImg { get; set; }

    public string? MailHtmlContent { get; set; }

    public bool IsDeleted { get; set; }
}
