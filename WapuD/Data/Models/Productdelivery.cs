namespace WapuD.Data.Models;

public partial class Productdelivery
{
    public int DeliveryId { get; set; }

    public string DeliveryName { get; set; } = null!;

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
