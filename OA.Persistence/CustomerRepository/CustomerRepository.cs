using ECom.Domain.Entities;
using ECom.Persistence.Seeds;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECom.Persistence.CustomerRepository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly InMemoryDbContext _context;

        public CustomerRepository(InMemoryDbContext context)
        {
            _context = context;
        }

        public Customer Add(Customer Customer)
        {
            _context.Customers.Add(Customer);
            _context.SaveChanges();
            return Customer;
        }

        public Customer GetById(int id)
        {
            _ = GetCustomerInMemory();
            return _context.Customers.Find(id);
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            _ = GetCustomerInMemory();
            return await _context.Customers.ToListAsync();
        }
       public Customer Update(Customer customer)
        {
            _context.Customers.Update(customer);
            _context.SaveChanges();
            return customer;
        }

        public bool Delete(int id)
        {
            _ = GetCustomerInMemory();
            var customer = _context.Customers.Find(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public async Task GetCustomerInMemory()
        {
            await SeedData.Seed(_context).ConfigureAwait(false);
        }

    }
}
