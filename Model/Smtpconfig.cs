using System;
using System.Collections.Generic;

namespace ECommerceWeb.Model;

public partial class Smtpconfig
{
    public int SmtpId { get; set; }

    public string SmtpUser { get; set; } = null!;

    public string SmtpPassword { get; set; } = null!;

    public string Host { get; set; } = null!;

    public int Port { get; set; }

    public bool IsEnableSsl { get; set; }

    public bool? IsDeleted { get; set; }
}
