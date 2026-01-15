namespace EventeiApi.Data.Tenant
{
    public interface ITenantDbContextFactory
    {
        TenantDbContext Create();
    }

}
