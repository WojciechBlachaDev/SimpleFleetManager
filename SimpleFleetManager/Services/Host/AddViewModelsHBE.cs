using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleFleetManager.ViewModels.Main;

namespace SimpleFleetManager.Services.Host
{
    public static class AddViewModelsHBE
    {
        public static IHostBuilder AddViewModels(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {
                services.AddTransient<MainWindowViewModel>();
                services.AddTransient<LoginPageViewModel>();
                services.AddTransient<UsersManagerPageViewModel>();
                services.AddTransient<ForkliftsManagerPageViewModel>();
            });
            return hostBuilder;
        }
    }
}
