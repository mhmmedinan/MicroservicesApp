using Core.Persistence.Repositories;
using Domain;

namespace Application.Services.Repositories;

public interface IIndividualCustomerRepository:IAsyncRepository<IndividualCustomer,Guid>
{
}
