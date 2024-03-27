using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleFleetManager.Models.Common.AMR.Misc;
using SimpleFleetManager.Models.Main;
using SimpleFleetManager.Services.Communication;
using SimpleFleetManager.Services.Data;
using SimpleFleetManager.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFleetManager.Services.Host
{
    public static class AddServicesHBE
    {
        public static IHostBuilder AddServices(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {
                services.AddSingleton<User>();
                services.AddTransient<Forklift>();
                services.AddSingleton<JobStep>();
                services.AddSingleton<Job>();
                services.AddSingleton<Location>();
                services.AddTransient<ForkliftLog>();
                services.AddSingleton<List<Forklift>>(provider => []);
                services.AddScoped<UserDataService>();
                services.AddScoped<ForkliftDataService>();
                services.AddScoped<ForkliftConnection>();
                services.AddScoped<JobStepDataService>();
                services.AddScoped<LocationDataService>();
                services.AddScoped<LogsDataService>();
                services.AddScoped<JobDataService>();
                services.AddSingleton<UserStore>();
                services.AddTransient<IDataService<User>, UserDataService>();
                services.AddTransient<IDataService<Forklift>, ForkliftDataService>();
                services.AddTransient<IDataService<JobStep>, JobStepDataService>();
                services.AddTransient<IDataService<Job>, JobDataService>();
                services.AddTransient<IDataService<Location>, LocationDataService>();
                services.AddSingleton<IUserStore, UserStore>();
                services.AddTransient<IForkliftConnection, ForkliftConnection>(provider => new ForkliftConnection());
                services.AddTransient<IDataService<ForkliftLog>, LogsDataService>();
            });
            return hostBuilder;
        }
    }
}
