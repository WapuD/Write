namespace WapuD.Data.Models;

public partial class Productcategory
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
