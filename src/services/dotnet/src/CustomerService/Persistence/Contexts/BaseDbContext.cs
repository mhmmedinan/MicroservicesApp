using Core.Persistence.Contexts;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Persistence.Contexts;

public class BaseDbContext : EfDbContextBase
{
    protected IConfiguration Configuration { get; set; }

    public BaseDbContext(DbContextOptions<BaseDbContext> options,IConfiguration configuration) : base(options)
    {
        Configuration = configuration;
    }

    public BaseDbContext(DbContextOptions<BaseDbContext> options)
    : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<IndividualCustomer> IndividualCustomers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
