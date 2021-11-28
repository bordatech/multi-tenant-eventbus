using DotNetCore.CAP.Filter;
using Sample.Api.EventBus;

namespace Sample.Api.Tenancy
{
    public class TenantServiceEventBusInterceptor : EventBusInterceptor
    {
        private readonly ITenantService _tenantService;

        public TenantServiceEventBusInterceptor(ITenantService tenantService)
        {
            _tenantService = tenantService;
        }
        
        public override void OnSubscribeExecuting(ExecutingContext context)
        {
            string tenantId = context.DeliverMessage.Headers[nameof(_tenantService.TenantId)];
            ((TenantService)_tenantService).SetTenantId(tenantId);
        }
    }
}