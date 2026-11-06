using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Orders.Application.Inventory;
using Orders.Infrastructure.Clients.Inventory;
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

        if (configuration.GetValue<bool>($"{InventoryClientOptions.Section}:UseInMemory"))
        {
            services.AddSingleton<IInventoryClient, InMemoryInventoryClient>();
        }
        else
        {
            services.AddHttpClient<IInventoryClient, InventoryClient>((sp, client) =>
                {
                    var options = sp.GetRequiredService<IOptions<InventoryClientOptions>>();
                    client.BaseAddress = new Uri(options.Value.BaseUrl);
                })
                .AddHttpMessageHandler<CorrelationIdHandler>()
                .AddStandardResilienceHandler();
        }

        services.AddScoped<IOrderRepository, InMemoryOrderRepository>();
    }
}