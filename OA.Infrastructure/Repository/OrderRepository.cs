using ECom.Domain.Contract;
using ECom.Domain.Entities;
using ECom.Infrastructure.Persistance.Seeds;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECom.Infrastructure.Persistance.OrderRepository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly InMemoryDbContext _context;

        public OrderRepository(InMemoryDbContext context)
        {
            _context = context;
        }

        public Order AddOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
            return order;
        }

        public Order GetOrderById(int id)
        {
            _ = GetOrderInMemory();
            return _context.Orders.Find(id);
        }

        public async Task<IEnumerable<Order>> GetAllOrders()
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
            await SeedData.Seed(_context).ConfigureAwait(false);
        }

    }
}