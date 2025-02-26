﻿using System;
using System.Collections.Generic;

namespace ECommerceWeb.Model;

public partial class AddressType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool? IsDeleted { get; set; }
}
