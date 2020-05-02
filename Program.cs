using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PiController.ShellCommands;
using PiController.Features.SystemServices;
using PiController.Menu;
using PiController.Settings;
using PiController.SSH;

namespace PiController
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = ConfigureServices();

            var mainMenu = serviceProvider.GetService<MainMenu>();

            mainMenu.Start();
        }

        private static ServiceProvider ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();
            var config = BuildConfig();
            
            serviceCollection
                .AddSingleton<AppSettings>(config.Get<AppSettings>())
                .AddSingleton<Client>()
                .AddSingleton<StatusOptionMenu>()
                .AddSingleton<CommandFactory>()
                .AddSingleton<ServiceManager>()
                .AddSingleton<MainMenu>();

            return serviceCollection.BuildServiceProvider();
        }

        private static IConfiguration BuildConfig()
        {
            var configBuilder = 
                new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);
    
            return configBuilder.Build();
        }
    }
}
