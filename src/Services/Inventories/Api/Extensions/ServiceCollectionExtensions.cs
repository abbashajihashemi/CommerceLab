using Inventories.Api.Applications;

namespace Inventories.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApiServices(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddHealthChecks();
        services.AddVersioningApis();

        services.AddScoped<IVendorStockService, InMemoryVendorStockService>();
    }
}
