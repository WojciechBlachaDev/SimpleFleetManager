using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Configuration;
using SimpleFleetManager.Services.Host;
using SimpleFleetManager.ViewModels;
using System.Windows;

namespace SimpleFleetManager
{
    public partial class App : Application
    {
        private readonly IHost _host;
        public App()
        {
            Console.WriteLine("DebugConsoleStarted");
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("/applog.txt", rollingInterval: RollingInterval.Day)
                .WriteTo.Console() // Wyświetlanie logów o poziomie Debug w konsoli
                .CreateLogger();
            Log.Debug("Logger started");
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
