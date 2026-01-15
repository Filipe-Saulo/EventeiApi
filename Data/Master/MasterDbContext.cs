
using EventeiApi.Data.Master.Configurations;
using EventeiApi.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace EventeiApi.Data.Master
{
    public class MasterDbContext : DbContext
    {
        public MasterDbContext(DbContextOptions<MasterDbContext> options)
            : base(options) { }

        public DbSet<Company> Companies => Set<Company>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CompanyConfiguration());
        }
    }

}
