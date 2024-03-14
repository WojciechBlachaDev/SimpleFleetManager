using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

namespace SimpleFleetManager.Services.Host
{
    public static class AddConfigurationHBE
    {
        public static IHostBuilder AddConfig(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureAppConfiguration(c =>
            {
                c.AddJsonFile("Models/AppData/AppConfig.json");
                c.AddEnvironmentVariables();
            });
            return hostBuilder;
        }
    }
}