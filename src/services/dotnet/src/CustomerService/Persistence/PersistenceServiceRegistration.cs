using Application.Services.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using Persistence.Repositories;

namespace Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddDbContext<BaseDbContext>(op=>op.UseSqlServer(configuration.GetConnectionString("BaseDb")));

        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IIndividualCustomerRepository, IndividualCustomerRepository>();

        return services;
    }
}
