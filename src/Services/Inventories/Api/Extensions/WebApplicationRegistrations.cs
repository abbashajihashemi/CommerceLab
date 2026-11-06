namespace Inventories.Api.Extensions;

internal static class WebApplicationRegistrations
{
    public static void UseApiPipeline(this WebApplication app)
    {
        app.MapControllers();
        app.MapHealthChecks("/health");
    }
}