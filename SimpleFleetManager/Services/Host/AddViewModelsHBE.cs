using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleFleetManager.ViewModels;

namespace SimpleFleetManager.Services.Host
{
    public static class AddViewModelsHBE
    {
        public static IHostBuilder AddViewModels(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {
                services.AddTransient<MainWindowViewModel>();
            });
            return hostBuilder;
        }
    }
}
