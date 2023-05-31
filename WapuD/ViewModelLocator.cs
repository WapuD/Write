namespace WapuD
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
            services.AddTransient<MainWindowViewModel>();
            services.AddTransient<SignInViewModel>();
            services.AddTransient<SignUpViewModel>();
            services.AddTransient<BrowseProductViewModel>();
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
        public MainWindowViewModel? MainWindowViewModel => _provider?.GetRequiredService<MainWindowViewModel>();
        public SignInViewModel? SignInViewModel => _provider?.GetRequiredService<SignInViewModel>();
        public SignUpViewModel? SignUpViewModel => _provider?.GetRequiredService<SignUpViewModel>();
        public BrowseProductViewModel? BrowseProductViewModel => _provider?.GetRequiredService<BrowseProductViewModel>();
    }
}
