namespace EventeiApi.Data.Tenant.Resolver
{
    public class TenantResolver : ITenantResolver
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TenantResolver(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetSchema()
        {
            var user = _httpContextAccessor.HttpContext?.User;

            var schema = user?.FindFirst("schema")?.Value;

            if (string.IsNullOrEmpty(schema))
                throw new UnauthorizedAccessException("Tenant não identificado.");

            return schema;
        }
    }
}
