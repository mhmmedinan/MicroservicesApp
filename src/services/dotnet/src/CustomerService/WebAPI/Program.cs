using Core.CrossCuttingConcerns.Exceptions.Extensions;
using Core.ServiceDiscovery.Consul;
using Persistence;
using Steeltoe.Extensions.Configuration.ConfigServer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistenceServices(builder.Configuration);

builder.AddConfigServer();
builder.Services.AddConsulServiceDiscovery(builder.Configuration, options =>
{
    options.Tags = new[] { "customerservice", "api" };
    options.DisableAgentCheck = false;
});
builder.Services.AddCors();
builder.Services.AddControllers();

var app = builder.Build();

const string SERVICE_NAME = "CustomerService";

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.ConfigureCustomExceptionMiddleware();
}

if (app.Environment.IsProduction())
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


app.MapControllers();


app.Run();
