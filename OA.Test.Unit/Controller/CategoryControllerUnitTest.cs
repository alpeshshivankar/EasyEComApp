using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using OA.Controllers;
using OA.Domain.Entities;
using OA.Persistence;
using OA.Service.Contract;
using System.Collections.Generic;
using System.Linq;

namespace OA.Test.Unit.Controller
{
    [TestFixture]
    public class CategoryControllerUnitTest
    {
        Category c1;
        Category c2;
        Category c3;
        List<Category> categories;
        CategoryController categoryController;
        Mock<ApplicationDbContext> applicationDbContextMock;
        Mock<ICategoryService> categoryServiceMock;
        public CategoryControllerUnitTest()
        {
            c1 = new Category
            {
                Id = 1,
                CategoryName = "Dairy",
                Description = "Different products generated from Milk",
                Products = new List<Product>{
                    new Product{ Id=1,ProductName="Cow Milk",UnitPrice= 5.25M },
                    new Product{ Id=2,ProductName="Ship Milk",UnitPrice= 7.5M }
                }
            };
            c2 = new Category {
                Id = 1,
                CategoryName = "Vegetables",
                Description = "Different products generated from Plants",
                Products = new List<Product>{
                    new Product{ Id=3,ProductName="Spinach",UnitPrice= 1.1M },
                    new Product{ Id=4,ProductName="Carrot",UnitPrice= 0.5M }
                }
            };
            c3 = new Category {
                Id = 1,
                CategoryName = "Bakery",
                Description = "Different products generated in Bakery",
                Products = new List<Product>{
                    new Product{ Id=5,ProductName="Bread",UnitPrice= 0.85M },
                    new Product{ Id=6,ProductName="Cake",UnitPrice= 5.5M }
                }
            };

            categories = new List<Category> { c1, c2 };
            applicationDbContextMock = new Mock<ApplicationDbContext>();
            categoryController = new CategoryController(categoryServiceMock.Object);
        }
        [Test]
        public void Get_List_Of_Categories()
        {
            //setup 
            applicationDbContextMock.Setup(db => db.Categories.ToList()).Returns(categories);

            var result = categoryController.GetAllCategories() as ViewResult;
            var model = result.Model as List<Category>;

            CollectionAssert.Contains(model, c1);
            CollectionAssert.Contains(model, c2);
        }
    }
}
