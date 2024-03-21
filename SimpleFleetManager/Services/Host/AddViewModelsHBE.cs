using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleFleetManager.ViewModels.ForkliftPages;
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
                services.AddTransient<ForkliftsPageViewModel>();
                services.AddTransient<ActualParametersPageVIewModel>();
                services.AddSingleton<ErrorsPageViewModel>();
                services.AddSingleton<SafetyPageViewModel>();
                services.AddSingleton<ScangridsPageViewModel>();
                services.AddSingleton<SickApiPageViewModel>();
                services.AddSingleton<WorkPageViewModel>();
            });
            return hostBuilder;
        }
    }
}
