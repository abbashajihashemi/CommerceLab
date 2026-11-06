using Serilog;

namespace Inventories.Api.Extensions;

public static class LoggingExtensions
{
    public static void UseSerilogLogging(this ConfigureHostBuilder host)
    {
        host.UseSerilog((context, services, configuration) => configuration
            .ReadFrom.Configuration(context.Configuration)
            .ReadFrom.Services(services)
            .Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .Enrich.WithThreadId()
            .Enrich.WithProperty("Application", "Inventories.Api")
            .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
        );
    }
}