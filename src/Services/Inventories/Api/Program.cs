using Inventories.Api.Extensions;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    Log.Information("Starting Inventories API");
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilogLogging();
    builder.Services.AddApiServices();

    var app = builder.Build();
    app.UseApiPipeline();

    app.Run();
}
catch (Exception e)
{
    Log.Fatal(e, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}