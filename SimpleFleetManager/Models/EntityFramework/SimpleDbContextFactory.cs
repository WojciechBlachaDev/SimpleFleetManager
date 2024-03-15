using Microsoft.EntityFrameworkCore;

namespace SimpleFleetManager.Models.EntityFramework
{
    public class SimpleDbContextFactory(Action<DbContextOptionsBuilder> configure)
    {
        private readonly Action<DbContextOptionsBuilder> _configure = configure;
        public SimpleDbContext CreateDbContext()
        {
            DbContextOptionsBuilder<SimpleDbContext> options = new();
            options.UseSqlite("Dataset Source=Models/AppData/DataBase/Simple.db");
            _configure(options);
            return new SimpleDbContext(options.Options);
        }
    }
}
