using Inventories.Api.Logging;
using Serilog;

namespace Inventories.Api.Extensions;

internal static class WebApplicationRegistrations
{
    public static void UseApiPipeline(this WebApplication app)
    {
        app.UseRequestLogging();

        app.MapControllers();
        app.MapHealthChecks("/health");
    }

    public static void UseRequestLogging(this WebApplication app)
    {
        app.UseMiddleware<CorrelationIdMiddleware>();

        app.UseSerilogRequestLogging(options =>
        {
            options.GetLevel = (httpContext, _, ex) =>
            {
                if (httpContext.Request.Path.StartsWithSegments("/health"))
                    return Serilog.Events.LogEventLevel.Verbose;

                return ex != null || httpContext.Response.StatusCode >= 500
                    ? Serilog.Events.LogEventLevel.Error
                    : Serilog.Events.LogEventLevel.Information;
            };

            options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
            {
                diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
                diagnosticContext.Set("RequestScheme", httpContext.Request.Scheme);
                diagnosticContext.Set("UserAgent", httpContext.Request.Headers.UserAgent.ToString());
                diagnosticContext.Set("ClientIP", httpContext.Connection.RemoteIpAddress?.ToString());
            };
        });
    }
}
