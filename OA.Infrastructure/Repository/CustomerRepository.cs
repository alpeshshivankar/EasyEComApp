using ECom.Domain.Contract;
using ECom.Domain.Entities;
using ECom.Infrastructure.Persistance;
using ECom.Infrastructure.Persistance.Seeds;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECom.Infrastructure.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly InMemoryDbContext _context;

        public CustomerRepository(InMemoryDbContext context)
        {
            _context = context;
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

        public Customer AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return customer;
        }

        public Customer GetCustomerById(int id)
        {
            _ = GetCustomerInMemory();
            return _context.Customers.Find(id);
        }

        public async Task<IEnumerable<Customer>> GetAllCategories()
        {
            _ = GetCustomerInMemory();
            return await _context.Customers.ToListAsync();
        }
    }
}