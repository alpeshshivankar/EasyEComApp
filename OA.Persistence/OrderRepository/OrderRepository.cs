using ECom.Domain.Entities;
using ECom.Persistence.Seeds;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECom.Persistence.OrderRepository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly InMemoryDbContext _context;

        public OrderRepository(InMemoryDbContext context)                                                                                                                                                                                           
        {
            _context = context;
        }

        public Order Add(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
            return order;
        }

        public Order GetById(int id)
        {
            _ = GetOrderInMemory();
            return _context.Orders.Find(id);
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            _ = GetOrderInMemory();
            return await _context.Orders.ToListAsync();
        }
        public Order Update(Order order)
        {
            _context.Orders.Update(order);
            _context.SaveChanges();
            return order;
        }
        public bool Delete(int id)
        {
            _ = GetOrderInMemory();
            var order = _context.Orders.Find(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public async Task GetOrderInMemory()
        {
            await  SeedData.Seed(_context).ConfigureAwait(false);
        }
    }
}

