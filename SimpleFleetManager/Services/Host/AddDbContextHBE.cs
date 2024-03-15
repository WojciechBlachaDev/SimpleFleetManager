using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleFleetManager.Models.EntityFramework;

namespace SimpleFleetManager.Services.Host
{
    public static class AddDbContextHBE
    {
        public static IHostBuilder AddContext(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices((context, services) =>
            {
                string? connectionString = context.Configuration.GetConnectionString("sqlite");
                void configureDbContext(DbContextOptionsBuilder o) => o.UseSqlite(connectionString);
                services.AddDbContext<SimpleDbContext>(configureDbContext);
                services.AddSingleton<SimpleDbContextFactory>(new SimpleDbContextFactory(configureDbContext));
            });
            return hostBuilder;
        }
    }
}
