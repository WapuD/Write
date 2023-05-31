namespace WapuD.Services
{
    public class ProductService
    {
        private readonly TradeContext _tradeContext;
        public ProductService(TradeContext tradeContext)
        {
            _tradeContext = tradeContext;
        }

        public async Task<List<DB_Product>> GetProducts()
        {
            List<DB_Product> products = new();
            try
            {
                var product = await _tradeContext.Products.ToListAsync();
                //await _tradeContext.Pnames.ToListAsync();
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
                    //OrderAmmount += (item.Count * item.Price) - ((item.Count * item.Price) * item.Discount / 100);
                    //_orderAmmount += item.Count * item.Price;

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
                    //OrderAmmount += (item.Count * item.Price) - ((item.Count * item.Price) * item.Discount / 100);
                    //_orderAmmount += item.Count * item.Price;

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
                //item.OrderAmmount = test();

                //item.OrderAmmount = (float)test();
                //await _tradeContext.SaveChangesAsync();

                //item.OrderDiscountAmmount = (float)test2();
                //await _tradeContext.SaveChangesAsync();
            }
        }

        public async Task GetListFullInformation()
        {
            
        }
    }
}
