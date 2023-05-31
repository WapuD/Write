namespace WapuD.Data.Models;

public partial class Orderinfo
{
    public int OrderId { get; set; }

    public DateOnly OrderDate { get; set; }

    public DateOnly OrderDeliveryDate { get; set; }

    public int OrderPickupPoint { get; set; }

    public string? OrderFio { get; set; }

    public int OrderCode { get; set; }

    public int OrderStatus { get; set; }

    public virtual Pointsget OrderPickupPointNavigation { get; set; } = null!;

    public virtual ICollection<Orderproduct> Orderproducts { get; } = new List<Orderproduct>();
}
