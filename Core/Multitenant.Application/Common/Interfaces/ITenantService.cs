using Multitenant.Domain.Settings;

namespace Multitenant.Application.Common.Interfaces
{
    public interface ITenantService
    {
        string GetDatabaseProvider();
        string GetConnectionString();
        Tenant GetTenant();
    }
}
