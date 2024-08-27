using Microsoft.EntityFrameworkCore;
using ECom.Domain.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ECom.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        // This constructor is used of runit testing
        public ApplicationDbContext()
        {

        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
            new Category
            {
                Id = 1,
                CategoryName = "Dairy",
                Description = "Different products generated from Milk"
            },
            new Category
            {
                Id = 2,
                CategoryName = "Vegetables",
                Description = "Different products generated from Plants"
            },
            new Category
            {
                Id = 3,
                CategoryName = "Bakery",
                Description = "Different products generated in Bakery"
            });
            modelBuilder.Entity<Product>().HasData(
                    new Product { Id = 1, ProductName = "Cow Milk", UnitPrice = 5.25M, CategoryId = 1 },
                    new Product { Id = 2, ProductName = "Ship Milk", UnitPrice = 7.5M, CategoryId = 1 },
                    new Product { Id = 3, ProductName = "Spinach", UnitPrice = 1.1M, CategoryId = 2 },
                    new Product { Id = 4, ProductName = "Carrot", UnitPrice = 0.5M, CategoryId = 2 },
                    new Product { Id = 5, ProductName = "Bread", UnitPrice = 0.85M, CategoryId = 3 },
                    new Product { Id = 6, ProductName = "Cake", UnitPrice = 5.5M, CategoryId = 3 }
            );

            modelBuilder.Entity<OrderDetail>().HasKey(o => new { o.OrderId, o.ProductId });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                .UseSqlServer("DataSource=app.db");
            }

        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
