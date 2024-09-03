using ECom.Infrastructure.Persistance.DataModels;
using Microsoft.EntityFrameworkCore;

namespace ECom.Infrastructure.Persistance
{
    public class InMemoryDbContext : DbContext
    {
        public InMemoryDbContext(DbContextOptions<InMemoryDbContext> options) : base(options)
        {
        }

        //public DbSet<Product> Products { get; set; }
        //public DbSet<Customer> Customers { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
        //public DbSet<Order> Orders { get; set; }
    }
}
