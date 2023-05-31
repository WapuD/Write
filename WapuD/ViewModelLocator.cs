using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WapuD
{
    internal class ViewModelLocator
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

            services.AddTransient<MainWindowViewModel>();
            services.AddTransient<SignInViewModel>();
            services.AddTransient<SignUpViewModel>();

            services.AddSingleton<PageService>();
            services.AddSingleton<UserService>();
            services.AddSingleton<DocumentService>();

            services.AddDbContext<TradeContext>(options =>
            {
                var connd = _configuration.GetConnectionString("LocalConnection");
                options.UseMySql(connd, ServerVersion.AutoDetect(connd));
            }, ServiceLifetime.Singleton);

            _provider = services.BuildServiceProvider();
        }
        public MainWindowViewModel? MainWindowViewModel => _provider?.GetRequiredService<MainWindowViewModel>();
        public SignInViewModel? SignInViewModel => _provider?.GetRequiredService<SignInViewModel>();
        public SignUpViewModel? SignUpViewModel => _provider?.GetRequiredService<SignUpViewModel>();
    }
}
