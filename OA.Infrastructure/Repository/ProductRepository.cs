using ECom.Domain.Contract;
using ECom.Domain.Entities;
using ECom.Infrastructure.Persistance;
using ECom.Infrastructure.Persistance.Seeds;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECom.Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly InMemoryDbContext _context;

        public ProductRepository(InMemoryDbContext context)
        {
            _context = context;
        }

        public Product AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return product;
        }

        public Product GetProductById(int id)
        {
            _ = GetProductInMemory();
            return _context.Products.Find(id);
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            _ = GetProductInMemory();
            return await _context.Products.ToListAsync();
        }

        public Product Update(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
            return product;
        }

        public bool Delete(int id)
        {
            _ = GetProductInMemory();
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task GetProductInMemory()
        {
            await SeedData.Seed(_context).ConfigureAwait(false);
        }

    }
}