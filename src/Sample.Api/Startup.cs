using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sample.Api.EventBus;
using Sample.Api.Tenancy;
using Savorboard.CAP.InMemoryMessageQueue;

namespace Sample.Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddCap(opt =>
            {
                opt.UseInMemoryStorage();
                opt.UseInMemoryMessageQueue();
            }).AddSubscribeFilter<EventBusInterceptorFilter>();
            
            services.AddScoped<ITenantService, TenantService>();
            services.AddScoped<EventBusInterceptor, TenantServiceEventBusInterceptor>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}