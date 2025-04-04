using Core.CrossCuttingConcerns.Exceptions.Extensions;
using Core.ServiceDiscovery.Consul;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using Ocelot.Provider.Polly;
using Steeltoe.Extensions.Configuration.ConfigServer;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost
    .ConfigureAppConfiguration((hostingContext, config) =>
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
// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

const string SERVÝCE_NAME = "ApiGateway";

if (app.Environment.IsDevelopment())
    app.ConfigureCustomExceptionMiddleware();
if (app.Environment.IsProduction())
    app.ConfigureCustomExceptionMiddleware();

app.UseCors(b => b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseRouting();
app.UseStaticFiles();

app.UseEndpoints(config =>
{
    config.MapGet("/", async context =>
    {
        await context.Response.WriteAsync(SERVÝCE_NAME);
    });
    config.MapGet("/info", async context =>
    {
        await context.Response.WriteAsync($"{SERVÝCE_NAME},running on {context.Request.Host}");
    });
    config.MapGet("/api/values/health", () => Results.Ok("Healthy"));

});

app.UseOcelot().Wait();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();


