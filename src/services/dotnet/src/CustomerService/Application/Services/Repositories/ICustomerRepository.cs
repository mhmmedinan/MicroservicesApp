using Core.Persistence.Repositories;
using Domain;

namespace Application.Services.Repositories;

public interface ICustomerRepository:IAsyncRepository<Customer,Guid>
{
}
