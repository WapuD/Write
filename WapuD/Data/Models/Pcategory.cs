﻿namespace WapuD.Data.Models;

public partial class Pcategory
{
    public int PcategoryId { get; set; }

    public string ProductCategory { get; set; } = null!;

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
