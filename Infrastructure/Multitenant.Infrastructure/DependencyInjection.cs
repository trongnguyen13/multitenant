using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Multitenant.Application.Common.Interfaces;
using Multitenant.Domain.Settings;
using Multitenant.Infrastructure.Persistence;
using Multitenant.Infrastructure.Services;

namespace Multitenant.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddDbContext<ApplicationDbContext>(options =>
            //       options.UseSqlServer(
            //           configuration.GetConnectionString("DefaultConnection"),
            //           b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.Configure<TenantSettings>(configuration.GetSection("TenantSettings"));

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<ITenantService, TenantService>();
            services.AddTransient<IApplicationDbContext, ApplicationDbContext>();

            services.AddTenantMigration(configuration);

            return services;
        }
    }
}
