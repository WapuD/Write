﻿namespace WapuD.Data.Models;

public partial class Orderproduct
{
    public int OrderId { get; set; }

    public string ProductArticleNumber { get; set; } = null!;

    public int? ProductCount { get; set; }

    public virtual Orderinfo Order { get; set; } = null!;

    public virtual Product ProductArticleNumberNavigation { get; set; } = null!;
}
