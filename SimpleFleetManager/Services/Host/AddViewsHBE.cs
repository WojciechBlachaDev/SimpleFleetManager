using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleFleetManager.ViewModels.ForkliftPages;
using SimpleFleetManager.ViewModels.Main;
using SimpleFleetManager.Views.ForkliftPages;
using SimpleFleetManager.Views.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            });
            return hostBuilder;
        }
    }
}
