using ECom.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECom.Domain.Contract
{
    public interface ICustomerRepository
    {
        Customer AddCustomer(Customer customer);

        Customer GetCustomerById(int id);

        Task<IEnumerable<Customer>> GetAllCategories();
    }
}