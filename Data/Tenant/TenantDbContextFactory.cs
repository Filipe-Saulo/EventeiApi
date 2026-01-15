using EventeiApi.Data.Tenant.Resolver;
using Microsoft.EntityFrameworkCore;

namespace EventeiApi.Data.Tenant
{
    public class TenantDbContextFactory : ITenantDbContextFactory
    {
        private readonly IConfiguration _configuration;
        private readonly ITenantResolver _tenantResolver;

        public TenantDbContextFactory(
            IConfiguration configuration,
            ITenantResolver tenantResolver)
        {
            _configuration = configuration;
            _tenantResolver = tenantResolver;
        }

        public TenantDbContext Create()
        {
            var schema = _tenantResolver.GetSchema();

            var baseConn = _configuration.GetConnectionString("DefaultConnection");

            var conn = $"{baseConn};Database={schema}";

            var options = new DbContextOptionsBuilder<TenantDbContext>()
                .UseMySql(conn, ServerVersion.AutoDetect(conn))
                .Options;

            return new TenantDbContext(options);
        }
    }
}
