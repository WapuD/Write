namespace WapuD.Data.Models;

public partial class Productmanufacture
{
    public int ManufactureId { get; set; }

    public string ManufactureName { get; set; } = null!;

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
