using Core.CrossCuttingConcerns.Exceptions.Extensions;
using Core.ServiceDiscovery.Consul;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using Ocelot.Provider.Polly;
using Steeltoe.Extensions.Configuration.ConfigServer;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureAppConfiguration((hostingContext, config) =>
{
    var env = hostingContext.HostingEnvironment;
    config
        .AddJsonFile("appsettings.json", true, true)
        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
        .AddJsonFile("ocelot.json", optional: false, reloadOnChange: false)
        .AddJsonFile($"ocelot.{env.EnvironmentName}.json", optional: true)
        .AddConfigServer(env)
        .AddEnvironmentVariables();
});

builder.AddConfigServer();
builder.Services.AddConsulServiceDiscovery(builder.Configuration, options =>
{
    options.Tags = new[] { "gateway", "api" };
    options.DisableAgentCheck = false;
});
builder.Services.AddCors();
builder.Services.AddOcelot()
    .AddConsul()
    .AddPolly();
builder.Services.AddControllers();

var app = builder.Build();

const string SERVICE_NAME = "ApiGateway";

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
    app.ConfigureCustomExceptionMiddleware();

app.UseCors(b => b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseRouting();
app.UseStaticFiles();

app.UseEndpoints(config =>
{
    config.MapGet("/", async context =>
    {
        await context.Response.WriteAsync(SERVICE_NAME);
    });
    config.MapGet("/info", async context =>
    {
        await context.Response.WriteAsync($"{SERVICE_NAME}, running on {context.Request.Host}");
    });
    config.MapGet("/api/values/health", () => Results.Ok("Healthy"));
});

app.UseHttpsRedirection();
app.MapControllers();

app.Lifetime.ApplicationStarted.Register(() =>
{
    app.Logger.LogInformation("🚀 ApiGateway started successfully!");
});

app.Lifetime.ApplicationStopped.Register(() =>
{
    app.Logger.LogInformation("🛑 ApiGateway is shutting down...");
});

await app.UseOcelot();

app.Run();
