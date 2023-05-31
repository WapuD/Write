namespace WapuD.Data.Models;

public partial class Product
{
    public string ProductArticleNumber { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public string ProductDescription { get; set; } = null!;

    public int ProductCategory { get; set; }

    public string? ProductPhoto { get; set; }

    public int ProductManufacturer { get; set; }

    public int ProductSupplier { get; set; }

    public float ProductCost { get; set; }

    public int ProductDiscountAmount { get; set; }

    public int ProductDiscountMax { get; set; }

    public int ProductQuantityInStock { get; set; }

    public string ProductUnit { get; set; } = null!;

    public virtual ICollection<Orderproduct> Orderproducts { get; } = new List<Orderproduct>();

    public virtual Productcategory ProductCategoryNavigation { get; set; } = null!;

    public virtual Productmanufacture ProductManufacturerNavigation { get; set; } = null!;

    public virtual Productdelivery ProductSupplierNavigation { get; set; } = null!;
}
