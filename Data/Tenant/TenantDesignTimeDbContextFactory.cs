using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace EventeiApi.Data.Tenant
{
    public class TenantDesignTimeDbContextFactory : IDesignTimeDbContextFactory<TenantDbContext>
    {
        public TenantDbContext CreateDbContext(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<TenantDbContext>();
            
            var conn = config.GetConnectionString("DefaultConnection");
            optionsBuilder.UseMySql(conn, ServerVersion.AutoDetect(conn));

            return new TenantDbContext(optionsBuilder.Options);
        }
    }
}
