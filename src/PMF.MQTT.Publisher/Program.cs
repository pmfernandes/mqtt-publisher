namespace PMF.MQTT.Publisher
{
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using PMF.MQTT.Publisher.Hosting;
    using PMF.MQTT.Publisher.Settings;

    public static class Program
    {
        private static async Task Main(string[] args)
        {
            await Host
                .CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((builderContext, config) =>
                {
                    var basePath = Directory.GetCurrentDirectory();

                    config.SetBasePath(basePath)
                        .AddJsonFile("conf/appsettings.json")
                        .AddEnvironmentVariables();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    var applicationSettings = hostContext.Configuration.GetSection("ApplicationSettings").Get<ApplicationSettings>();

                    Configuration.Dependencies.ConfigureDependencies(services, applicationSettings);

                    services.AddHostedService<ConsoleHostedService>();

                })
                .RunConsoleAsync();
        }
    }
}
