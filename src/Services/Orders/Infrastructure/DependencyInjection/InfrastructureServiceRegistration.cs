using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Orders.Application.Inventories;
using Orders.Infrastructure.Clients.Inventories;
using Orders.Infrastructure.Http;
using Orders.Infrastructure.Persistence;

namespace Orders.Infrastructure.DependencyInjection;

public static class InfrastructureServiceRegistration
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        services.AddTransient<CorrelationIdHandler>();

        services.AddOptions<InventoryClientOptions>()
            .BindConfiguration(InventoryClientOptions.Section)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddHttpClient<IInventoryClient, InventoryClient>((sp, client) =>
            {
                var options = sp.GetRequiredService<IOptions<InventoryClientOptions>>();
                client.BaseAddress = new Uri(options.Value.BaseUrl);
            })
            .AddHttpMessageHandler<CorrelationIdHandler>()
            .AddStandardResilienceHandler();

        services.AddScoped<IOrderRepository, InMemoryOrderRepository>();
    }
}