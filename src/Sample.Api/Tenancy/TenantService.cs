namespace Sample.Api.Tenancy
{
    public class TenantService : ITenantService
    {
        public string TenantId { get; set; }

        internal void SetTenantId(string tenantId)
        {
            TenantId = tenantId;
        }
    }
}