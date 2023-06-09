﻿namespace WapuD
{
    public class ViewModelLocator
    {
        private static ServiceProvider? _provider;
        private static IConfiguration? _configuration;
        public static void Init()
        {
            _configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json")
           .Build();

            var services = new ServiceCollection();

            #region ViewModel
            services.AddTransient<AdminBrowseProductViewModel>();
            services.AddTransient<BrowseProductViewModel>();
            services.AddTransient<CartViewModel>();
            services.AddTransient<EditAdminViewModel>();
            services.AddTransient<MainWindowViewModel>();
            services.AddTransient<SignInViewModel>();
            services.AddTransient<SignUpViewModel>();
            #endregion

            #region Services

            services.AddSingleton<PageService>();
            services.AddSingleton<UserService>();
            services.AddSingleton<ProductService>();
            services.AddSingleton<DocumentService>();

            #endregion

            #region Add_DB
            services.AddDbContext<TradeContext>(options =>
            {
                var connd = _configuration.GetConnectionString("LocalConnection");
                options.UseMySql(connd, ServerVersion.AutoDetect(connd));
            }, ServiceLifetime.Singleton);
            #endregion

            _provider = services.BuildServiceProvider();
        }
        public AdminBrowseProductViewModel? AdminBrowseProductViewModel => _provider?.GetRequiredService<AdminBrowseProductViewModel>();
        public BrowseProductViewModel? BrowseProductViewModel => _provider?.GetRequiredService<BrowseProductViewModel>();
        public CartViewModel? CartViewModel => _provider?.GetRequiredService<CartViewModel>();
        public EditAdminViewModel? EditAdminViewModel => _provider?.GetRequiredService<EditAdminViewModel>();
        public MainWindowViewModel? MainWindowViewModel => _provider?.GetRequiredService<MainWindowViewModel>();
        public SignInViewModel? SignInViewModel => _provider?.GetRequiredService<SignInViewModel>();
        public SignUpViewModel? SignUpViewModel => _provider?.GetRequiredService<SignUpViewModel>();
    }
}
