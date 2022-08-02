using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Multitenant.Domain.Settings;

namespace Multitenant.Infrastructure.Persistence
{
    public static class TenantMigration
    {
        public static IServiceCollection AddTenantMigration(this IServiceCollection services, IConfiguration configuration)
        {
            var options = configuration.GetSection(nameof(TenantSettings)).Get<TenantSettings>();
            var defaultConnectionString = options.Defaults?.ConnectionString;
            var defaultDbProvider = options.Defaults?.DBProvider;
            if(defaultDbProvider.ToLower() == "mssql")
            {
                services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
                       b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            }

            var tenants = options.Tenants;
            foreach(var tenant in tenants)
            {
                string connectionString = tenant.ConnectionString;
                if (string.IsNullOrEmpty(connectionString))
                {
                    connectionString = defaultConnectionString;
                }

                using var scope = services.BuildServiceProvider().CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.SetConnectionString(connectionString);
                if(dbContext.Database.GetMigrations().Count() > 0)
                {
                    dbContext.Database.Migrate();
                }
            }

            return services;
        }
    }
}
