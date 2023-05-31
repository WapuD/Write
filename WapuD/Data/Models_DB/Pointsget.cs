namespace WapuD.Data.Models;

public partial class Pointsget
{
    public int PointsGetId { get; set; }

    public int PointsGetIndex { get; set; }

    public string PointsGetCiti { get; set; } = null!;

    public string PointsGetStreet { get; set; } = null!;

    public int PointsGetNumber { get; set; }

    public virtual ICollection<Orderinfo> Orderinfos { get; } = new List<Orderinfo>();
}
