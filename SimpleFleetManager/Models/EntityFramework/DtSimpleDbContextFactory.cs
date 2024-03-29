using Microsoft.EntityFrameworkCore;
namespace SimpleFleetManager.Models.EntityFramework
{
    public class DtSimpleDbContextFactory
    {
        public static SimpleDbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<SimpleDbContext>();
            optionsBuilder.UseSqlite("Dataset Source=Models/AppData/DataBase/Simple.db");
            return new SimpleDbContext(optionsBuilder.Options);
        }
    }
}
