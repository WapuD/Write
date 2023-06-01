using WapuD.Data.Models;

namespace WapuD.Services
{
    public class ProductService
    {
        private readonly TradeContext _tradeContext;
        public ProductService(TradeContext tradeContext)
        {
            _tradeContext = tradeContext;
        }

        public async Task<List<DB_Product>> GetProductsAdmin()
        {
            List<DB_Product> products = new();
            try
            {
                var product = await _tradeContext.Products.ToListAsync();
                await _tradeContext.Productmanufactures.ToListAsync();
                await Task.Run(() =>
                {
                    foreach (var item in product)
                    {
                        products.Add(new DB_Product
                        {
                            Image = item.ProductPhoto == string.Empty ? "picture.png" : item.ProductPhoto,
                            Title = item.ProductName,
                            Description = item.ProductDescription,
                            Manufacturer = item.ProductManufacturerNavigation.ManufactureName,
                            Price = item.ProductCost,
                            Discount = item.ProductDiscountAmount,
                            Quantity = item.ProductQuantityInStock,
                            Article = item.ProductArticleNumber,
                            //Count = item.Count
                            //Status = item.ProductStatusActiv
                        });
                    }
                });
            }
            catch { }
            return products;
        }

        public async Task saveRedact(Orderinfo SelectedOrder, ObservableCollection<Orderinfo> Orders)
        {
            var item = Orders.First(i => i.OrderId == SelectedOrder.OrderId);
            var index = Orders.IndexOf(item);
            item.OrderDeliveryDate = DateOnly.FromDateTime(SelectedOrder.OrderDeliveryDate.ToDateTime(TimeOnly.FromDateTime(DateTime.Now)));
            item.OrderStatus = 2;
            Orders.RemoveAt(index);
            Orders.Insert(index, item);
            await _tradeContext.SaveChangesAsync();
        }

        public async Task saveNewDate(DateTime selectedDate, Orderinfo SelectedOrder, ObservableCollection<Orderinfo> Orders)
        {
            var item = Orders.First(i => i.OrderId == SelectedOrder.OrderId);
            var index = Orders.IndexOf(item);
            item.OrderDeliveryDate = DateOnly.FromDateTime(selectedDate);
            Orders.RemoveAt(index);
            Orders.Insert(index, item);
            await _tradeContext.SaveChangesAsync();
        }

        public async Task deleteProduct(Product SelectedProduct, ObservableCollection<Product> Products)
        {
            var item = Products.First(i => i.ProductArticleNumber == SelectedProduct.ProductArticleNumber);
            var index = Products.IndexOf(item);
            item.ProductPhoto = "del.png";
            //item.ProductStatusActiv = "Удален";
            Products.RemoveAt(index);
            Products.Insert(index, item);
            await _tradeContext.SaveChangesAsync();
        }

        public async Task addProduct(Product SelectedProduct)
        {
            await _tradeContext.AddAsync(new Product
            {   
                ProductArticleNumber = SelectedProduct.ProductArticleNumber,
                ProductName = SelectedProduct.ProductName,
                ProductDescription = SelectedProduct.ProductDescription,
                ProductCategory = SelectedProduct.ProductCategory,
                ProductPhoto = SelectedProduct.ProductPhoto,
                ProductManufacturer = SelectedProduct.ProductManufacturer,
                ProductSupplier = SelectedProduct.ProductSupplier,
                ProductCost = SelectedProduct.ProductCost,
                ProductDiscountAmount = SelectedProduct.ProductDiscountAmount,
                ProductDiscountMax = SelectedProduct.ProductDiscountMax,
                ProductQuantityInStock = SelectedProduct.ProductQuantityInStock,
                ProductUnit = SelectedProduct.ProductUnit
                //ProductStatus = SelectedProduct.ProductStatus,
                //ProductStatusActiv = "Активный"
            });
            await _tradeContext.SaveChangesAsync();
        }

        public async Task redactProduct(Product SelectedProduct, ObservableCollection<Product> Products)
        {
            var item = Products.First(i => i.ProductArticleNumber == SelectedProduct.ProductArticleNumber);
            var index = Products.IndexOf(item);

            item.ProductArticleNumber = SelectedProduct.ProductArticleNumber;
            item.ProductName = SelectedProduct.ProductName;
            item.ProductDescription = SelectedProduct.ProductDescription;
            item.ProductCategory = SelectedProduct.ProductCategory;
            item.ProductPhoto = SelectedProduct.ProductPhoto;
            item.ProductManufacturer = SelectedProduct.ProductManufacturer;
            item.ProductSupplier = SelectedProduct.ProductSupplier;
            item.ProductCost = SelectedProduct.ProductCost;
            item.ProductDiscountAmount = SelectedProduct.ProductDiscountAmount;
            item.ProductDiscountMax = SelectedProduct.ProductDiscountMax;
            item.ProductQuantityInStock = SelectedProduct.ProductQuantityInStock;
            item.ProductUnit = SelectedProduct.ProductUnit;
            //item.ProductStatus = SelectedProduct.ProductStatus;

            Products.RemoveAt(index);
            Products.Insert(index, item);
            await _tradeContext.SaveChangesAsync();
        }

        public List<Productcategory> getAllCategories()
        {
            return _tradeContext.Productcategories.ToList();
        }

        public int getMaxCategorie()
        {
            return _tradeContext.Productcategories.Max(i => i.CategoryId);
        }

        public async Task addCategories(string categorie)
        {
            await _tradeContext.AddAsync(new Productcategory
            {
                CategoryId = getMaxCategorie() + 1,
                CategoryName = categorie
            });
            await _tradeContext.SaveChangesAsync();
        }

        public async Task editCategorie(int SelectedCategorie, string manufacture, ObservableCollection<Productcategory> Categories)
        {
            var item = Categories.First(i => i.CategoryId == SelectedCategorie);
            var index = Categories.IndexOf(item);

            item.CategoryId = SelectedCategorie;
            item.CategoryName = manufacture;

            Categories.RemoveAt(index);
            Categories.Insert(index, item);
            await _tradeContext.SaveChangesAsync();
        }

        public List<Productmanufacture> getAllManufacrurers()
        {
            return _tradeContext.Productmanufactures.ToList();
        }

        public int getMaxManufacture()
        {
            return _tradeContext.Productmanufactures.Max(i => i.ManufactureId);
        }

        public async Task addManufature(string manufacture)
        {
            await _tradeContext.AddAsync(new Productmanufacture
            {
                ManufactureId = getMaxManufacture() + 1,
                ManufactureName = manufacture
            });
            await _tradeContext.SaveChangesAsync();
        }

        public async Task editManufature(int SelectedManufacture, string manufacture, ObservableCollection<Productmanufacture> Manufacturers)
        {
            var item = Manufacturers.First(i => i.ManufactureId == SelectedManufacture);
            var index = Manufacturers.IndexOf(item);

            item.ManufactureId = SelectedManufacture;
            item.ManufactureName = manufacture;

            Manufacturers.RemoveAt(index);
            Manufacturers.Insert(index, item);
            await _tradeContext.SaveChangesAsync();
        }

        public async Task<List<DB_Product>> GetProducts()
        {
            List<DB_Product> products = new();
            try
            {
                var product = await _tradeContext.Products.ToListAsync();
                await _tradeContext.Productcategories.ToListAsync();
                await _tradeContext.Productmanufactures.ToListAsync();
                await Task.Run(() =>
                {
                    foreach (var item in product)
                    {
                        products.Add(new DB_Product
                        {
                            Image = item.ProductPhoto == string.Empty ? "picture.png" : item.ProductPhoto,
                            Title = item.ProductName,
                            Description = item.ProductDescription,
                            Manufacturer = item.ProductManufacturerNavigation.ManufactureName,
                            Price = item.ProductCost,
                            Discount = item.ProductDiscountAmount,
                            Article = item.ProductArticleNumber,
                            Quantity = item.ProductQuantityInStock
                        });
                    }
                });
            }
            catch { }
            return products;
        }
        public async Task<List<DB_Product>> GetCart()
        {
            List<DB_Product> a = new();
            var b = await GetProducts();

            foreach (var item in Global.Cart)
            {
                var product = b.SingleOrDefault(c => c.Article.Equals(item.ArticleName));
                if (product != null)
                {
                    product.Count = Global.Cart.Single(a => a.ArticleName.Equals(product.Article)).Count;
                    a.Add(product); 
                }
            }
            return a;
        }

        public async Task<List<Pointsget>> GetPoints() => await _tradeContext.Pointsgets.AsNoTracking().ToListAsync();

        public async Task<int> AddOrder(Orderinfo order)
        {
            await _tradeContext.Orderinfos.AddAsync(order);
            await _tradeContext.SaveChangesAsync();

            foreach (var item in Global.Cart)
            {
                await _tradeContext.Orderproducts.AddAsync(new Orderproduct
                {
                    OrderId = order.OrderId,
                    ProductArticleNumber = item.ArticleName,
                    ProductCount = item.Count
                });
                await _tradeContext.SaveChangesAsync();
            }

            return order.OrderId;
        }
        public async Task<List<Orderinfo>> GetOrders()
        {
            await _tradeContext.Orderproducts.ToListAsync();
            return await _tradeContext.Orderinfos.ToListAsync();
        }
        public async Task UpdateAmmountOrder()
        {
            await _tradeContext.Orderproducts.ToListAsync();
            await _tradeContext.Products.ToListAsync();
            var currentList = await _tradeContext.Orderinfos.ToListAsync();

            foreach(var item in currentList)
            {
                Func<float?> test = ()=> 
                {
                    float? orderammount = 0;
                    float? _orderammount = 0;
                    foreach (var test1 in item.Orderproducts.ToList()) 
                    {
                        orderammount += (test1.ProductCount * test1.ProductArticleNumberNavigation.ProductCost) - ((test1.ProductCount * test1.ProductArticleNumberNavigation.ProductCost) * test1.ProductArticleNumberNavigation.ProductDiscountAmount / 100);
                        _orderammount += test1.ProductCount * test1.ProductArticleNumberNavigation.ProductCost;
                    }

                    orderammount = (float)Math.Round((decimal)orderammount, 2);
                    _orderammount = (float)Math.Round(((decimal)_orderammount - (decimal)orderammount), 2);
                    return orderammount;
                };

                Func<float?> test2 = () =>
                {
                    float? orderammount = 0;
                    float? _orderammount = 0;
                    foreach (var test1 in item.Orderproducts.ToList())
                    {
                        orderammount += (test1.ProductCount * test1.ProductArticleNumberNavigation.ProductCost) - ((test1.ProductCount * test1.ProductArticleNumberNavigation.ProductCost) * test1.ProductArticleNumberNavigation.ProductDiscountAmount / 100);
                        _orderammount += test1.ProductCount * test1.ProductArticleNumberNavigation.ProductCost;
                    }

                    orderammount = (float)Math.Round((decimal)orderammount, 2);
                    _orderammount = (float)Math.Round(((decimal)_orderammount - (decimal)orderammount), 2);
                    return _orderammount;
                };
                Debug.WriteLine(item.OrderId + " " + test() + " " + test2());
            }
        }

        public async Task<List<Productcategory>> getAllCategoriesObjects()
        {
            return await _tradeContext.Productcategories.ToListAsync();
        }

        public async Task<List<Productmanufacture>> getAllManufacrurersObjects()
        {
            return await _tradeContext.Productmanufactures.ToListAsync();
        }

        public Product getProd(string article)
        {
            return _tradeContext.Products.Where(i => i.ProductArticleNumber == article).First();
        }

        public ObservableCollection<Product> getAllProd()
        {
            return _tradeContext.Products.ToObservableCollection<Product>();
        }
    }
}
