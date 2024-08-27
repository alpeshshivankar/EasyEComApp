using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ECom.Controllers;
using ECom.Domain.Entities;
using ECom.Persistence;
using ECom.Service.Contract;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ECom.Test.Unit.Controller
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
                Description = "Different products generated from Milk"
            };
            c2 = new Category {
                Id = 2,
                CategoryName = "Vegetables",
                Description = "Different products generated from Plants"
            };
            c3 = new Category {
                Id = 3,
                CategoryName = "Bakery",
                Description = "Different products generated in Bakery"
            };

            categories = new List<Category> { c1, c2 };
            applicationDbContextMock = new Mock<ApplicationDbContext>();
            categoryServiceMock = new Mock<ICategoryService>();
            categoryController = new CategoryController();
        }
        [Test]
        public void Get_List_Of_Categories()
        {
            //setup 
            applicationDbContextMock.Setup(db =>  db.Categories.ToList()).Returns(categories);

            var result = categoryController.GetAll();
            var model = result.Result as List<Category>;

            CollectionAssert.Contains(model, c1);
            CollectionAssert.Contains(model, c2);
        }
    }
}
