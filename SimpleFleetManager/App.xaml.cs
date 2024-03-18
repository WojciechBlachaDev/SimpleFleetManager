using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using SimpleFleetManager.Services.Host;
using SimpleFleetManager.ViewModels.Main;
using System.IO;
using System.Reflection;
using System.Windows;

namespace SimpleFleetManager
{
    public partial class App : Application
    {
        private readonly IHost _host;
        public App()
        {
            try
            {
                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .WriteTo.Console()
                    .WriteTo.File("Models/AppData/appLog.txt", rollingInterval: RollingInterval.Month)
                    .WriteTo.File("C:\\Users\\Wojtek\\Desktop\\Projekty\\ProjektBR\\SimpleFleetManager\\SimpleFleetManager\\Models\\AppData\\debuglog.txt")
                    .Destructure.ToMaximumDepth(5)
                    .CreateLogger();
                Log.Information("Logger started");
            }
            catch (Exception ex)
            {
                Log.Error("Error occured while creating serilog configuration: " + ex.Message);
            }
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
            _host = CreateHostBuilder().Build();
        }
        public static IHostBuilder CreateHostBuilder(string[]? args = null)
        {
            return Host.CreateDefaultBuilder(args)
                .AddConfig()
                .AddContext()
                .AddServices()
                .AddViewModels()
                .AddViews();
        }
        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host.StartAsync();
            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.DataContext = _host.Services.GetRequiredService<MainWindowViewModel>();
            if (!MainWindow.IsLoaded)
            {
                MainWindow.Show();
            }
            base.OnStartup(e);
        }
        protected override async void OnExit(ExitEventArgs e)
        {
            await _host.StopAsync();
            _host.Dispose();
            base.OnExit(e);
        }
    }

}
