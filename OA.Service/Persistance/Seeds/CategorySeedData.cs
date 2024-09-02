using ECom.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.Application.Persistance.Seeds
{
    public class SeedData
    {
        public static async Task Seed(InMemoryDbContext context)
        {
            var categories = new List<Category>
            {
                new Category(){Id=1, CategoryName="Fruits & Vegetables",Description=""  },
                new Category(){Id=2, CategoryName="Dairy & Eggs",Description=""  },
                new Category(){Id=3, CategoryName="Meat & Seafood",Description=""  },
                new Category(){Id=4, CategoryName="Bakery",Description=""  },
                new Category(){Id=5, CategoryName="Pantry Staples",Description=""  },
                new Category(){Id=6, CategoryName="Beverages",Description=""  },
                new Category(){Id=7, CategoryName="Snacks & Confectionery",Description=""  },
                new Category(){Id=8, CategoryName="Frozen Foods",Description=""  },
                new Category(){Id=9, CategoryName="Cereals & Breakfast Foods",Description=""  },
                new Category(){Id=10, CategoryName="Health & Wellness",Description=""  }
            };

            var products = new List<Product>
            {
                new() { Id=1, ProductName="Apple", CategoryId=1, UnitPrice=1.1M},
                new() { Id=2, ProductName="Mango", CategoryId=1, UnitPrice=2.2M},
                new() { Id=3, ProductName="PineApple", CategoryId=1, UnitPrice=3.3M},
                new() { Id=4, ProductName="Milk", CategoryId=2, UnitPrice=1.1M},
                new() { Id=5, ProductName="Egg", CategoryId=2, UnitPrice=2.2M},
                new() { Id=6, ProductName="Butter", CategoryId=2, UnitPrice=3.3M},
                new() { Id=7, ProductName="Fish", CategoryId=3, UnitPrice=1.1M},
                new() { Id=8, ProductName="Lamb", CategoryId=3, UnitPrice=2.2M},
                new() { Id=9, ProductName="Pork", CategoryId=3, UnitPrice=3.3M},
                new() { Id=10, ProductName="Muffins", CategoryId=4, UnitPrice=1.1M},
                new() { Id=11, ProductName="Cake", CategoryId=4, UnitPrice=2.2M},
                new() { Id=12, ProductName="Pastery", CategoryId=4, UnitPrice=3.3M},

            };

            var customers = new List<Customer>
            {

                new()
                {
                    CustomerName ="Pattrick",
                    ContactName = "Pattrick DeCosta",
                    ContactTitle = "Mr",
                    Address = "11, Welington Street",
                    City = "London",
                    Region = "Liverpool",
                    PostalCode = "L18 3HS",
                    Country = "UK",
                    Phone = "589774455",
                    Fax = string.Empty,
                    CreatedBy = 1001,
                    CreatedDate = DateTime.Now.Date
                },
                new ()
                {
                    CustomerName ="Robert",
                    ContactName = "Robert",
                    ContactTitle = "Mr",
                    Address = "135,Churchil Street",
                    City = "vrindavan",
                    Region = "Lancaster",
                    PostalCode = "LA1 1AT",
                    Country = "India",
                    Phone = "815d8781124",
                    Fax = string.Empty,
                    CreatedBy = 1002,
                    CreatedDate = DateTime.Now.AddDays(-5).Date
                }
              };
            var orders = new List<Order>
            {
                new()
                {
                    Id=1,
                    CustomerId = 1,
                    OrderDate = DateTime.Now.AddDays(-3).Date,
                    OrderFulfillmentDate =null,
                    OrderStatus="Open",
                    ProductDetails = new List<Product>()
                    {
                        new()
                        {
                            Id=31,
                            ProductName = "Cake",
                            UnitPrice= 2.2M,
                            CategoryId = 4
                        }
                    },
                    CreatedDate = DateTime.Now.AddDays(-3).Date,
                },
                new()
                {
                    Id=2,
                    CustomerId = 2,
                    OrderDate = DateTime.Now.AddDays(-2).Date,
                    OrderFulfillmentDate =null,
                    OrderStatus="Completed",
                    ProductDetails = new List<Product>()
                    {
                        new()
                        {
                            Id=21,
                            ProductName = "Apple",
                            UnitPrice= 0.5M,
                            CategoryId = 1
                        },
                        new()
                        {
                            Id=22,
                            ProductName = "Egg",
                            UnitPrice= 4M,
                            CategoryId = 2
                        },
                        new()
                        {
                            Id=23,
                            ProductName = "Fish",
                            UnitPrice= 6M,
                            CategoryId = 3
                        }
                    },
                    CreatedDate = DateTime.Now.AddDays(-2).Date,
                }
             };

            if (context.Categories.Any())
            {
                return;
            }
            context.Categories.AddRange(categories);
            context.Products.AddRange(products);
            context.Customers.AddRange(customers);
            context.Orders.AddRange(orders);

            await context.SaveChangesAsync();
        }
    }
}
