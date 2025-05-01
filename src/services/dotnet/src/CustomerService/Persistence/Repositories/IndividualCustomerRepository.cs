using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class IndividualCustomerRepository : EfRepositoryBase<IndividualCustomer, Guid, BaseDbContext>, IIndividualCustomerRepository
{
    public IndividualCustomerRepository(BaseDbContext context) : base(context)
    {
    }
}
