using ECom.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECom.Persistence.CustomerRepository
{
    public interface ICustomerRepository
    {

        Customer Add(Customer Customer);
        Customer GetById(int id);
        Task<IEnumerable<Customer>> GetAll();
        Customer Update(Customer order);
        bool Delete(int id);

    }
}
