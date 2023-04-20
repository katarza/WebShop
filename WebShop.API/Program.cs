using WebShop.API;

using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("WebShop API starting");

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfiguration) => loggerConfiguration
     .WriteTo.Console()
     .ReadFrom.Configuration(context.Configuration));

var app = builder
       .ConfigureServices()
       .ConfigurePipeline();

app.UseSerilogRequestLogging();

app.Run();

public partial class Program { }
