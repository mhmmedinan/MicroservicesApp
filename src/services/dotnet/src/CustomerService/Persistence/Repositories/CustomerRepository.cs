using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class CustomerRepository : EfRepositoryBase<Customer, Guid, BaseDbContext>,ICustomerRepository
{
    public CustomerRepository(BaseDbContext context) : base(context)
    {
    }
}
