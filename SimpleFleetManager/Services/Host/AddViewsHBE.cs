using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleFleetManager.ViewModels.ForkliftPages;
using SimpleFleetManager.ViewModels.Main;
using SimpleFleetManager.Views.ForkliftPages;
using SimpleFleetManager.Views.Main;
namespace SimpleFleetManager.Services.Host
{
    public static class AddViewsHBE
    {
        public static IHostBuilder AddViews(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {
                services.AddSingleton<MainWindow>(s =>
                new MainWindow(s.GetRequiredService<MainWindowViewModel>()));
                services.AddSingleton<LoginPage>(s =>
                new LoginPage(s.GetRequiredService<LoginPageViewModel>()));
                services.AddSingleton<UsersManagerPage>(s =>
                new UsersManagerPage(s.GetRequiredService<UsersManagerPageViewModel>()));
                services.AddSingleton<ForkliftManagementPage>(s =>
                new ForkliftManagementPage(s.GetRequiredService<ForkliftsManagerPageViewModel>()));
                services.AddSingleton<ForkliftsPage>(s =>
                new ForkliftsPage(s.GetRequiredService<ForkliftsPageViewModel>()));
                services.AddSingleton<ActualParametersPage>(s =>
                new ActualParametersPage(s.GetRequiredService<ActualParametersPageVIewModel>()));
                services.AddSingleton<ErrorsPage>(s =>
                new ErrorsPage(s.GetRequiredService<ErrorsPageViewModel>()));
                services.AddSingleton<SafetyPage>(s =>
                new SafetyPage(s.GetRequiredService<SafetyPageViewModel>()));
                services.AddSingleton<SickApiPage>(s =>
                new SickApiPage(s.GetRequiredService<SickApiPageViewModel>()));
                services.AddSingleton<WorkPage>(s =>
                new WorkPage(s.GetRequiredService<WorkPageViewModel>()));
                services.AddSingleton<LogPage>(s =>
                new LogPage(s.GetRequiredService<LogsPageViewModel>()));
                services.AddSingleton<LocationsManagerPage>(s =>
                new LocationsManagerPage(s.GetRequiredService<LocationsManagerPageViewModel>()));
                services.AddSingleton<ManualTaskCreatorPage>(s =>
                new ManualTaskCreatorPage(s.GetRequiredService<ManualTasksCreatorPageViewModel>()));
            });
            return hostBuilder;
        }
    }
}
