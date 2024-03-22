using Microsoft.EntityFrameworkCore;
using SimpleFleetManager.Models.Common.AMR.Misc;
using SimpleFleetManager.Models.Common.AMR.Misc.Config;
using SimpleFleetManager.Models.Main;

namespace SimpleFleetManager.Models.EntityFramework
{
    public class SimpleDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Forklift> Forklifts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<JobStep> JobSteps { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<TebConfig> TebConfigs { get; set; }
        public DbSet<ForkliftLog> Logs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Forklift>()
                .HasOne(e => e.BackedUpTebConfig)
                .WithOne()
                .HasForeignKey<TebConfig>(e => e.ForkliftId);
            base.OnModelCreating(modelBuilder);
        }


    }
}
