using Orders.Application.DependencyInjection;
using Orders.Infrastructure.DependencyInjection;

namespace Orders.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddHealthChecks();
        services.AddExceptionHandlers();
        services.AddVersioningApis();

        services.AddApplicationServices();
        services.AddInfrastructure(configuration);
    }
}