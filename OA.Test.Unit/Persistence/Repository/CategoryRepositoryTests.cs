using AutoFixture;
using ECom.Domain.Entities;
using ECom.Persistence;
using ECom.Persistence.CategoryRepository;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ECom.Test.Unit.Persistence.Repository
{
    public class CategoryRepositoryTests
    {
        private readonly DbContextOptionsBuilder<InMemoryDbContext> dbContext;
        private readonly InMemoryDbContext _imDbContext;
        private readonly CategoryRepository _categoryRepository;
        public CategoryRepositoryTests()
        {
            
            dbContext = new DbContextOptionsBuilder<InMemoryDbContext>();
            dbContext.UseInMemoryDatabase(Guid.NewGuid().ToString());
            _imDbContext = new InMemoryDbContext(dbContext.Options);
            _categoryRepository = new CategoryRepository(_imDbContext);

        }
        [Fact]
        public void AddCategory_ShouldAddNewCategory()
        {
            var newCategory = new Category { Id = 13, CategoryName = "Clothing", Description = "Apparel items" };
            var response = _categoryRepository.Add(newCategory);
            response.Should().NotBeNull();
            response.CategoryName.Should().Be("Clothing");
            response.Description.Should().Be("Apparel items");
        }
        [Fact]
        public async Task GetAllCategories_ShouldReturnListOfCategories()
        {
            var response = await _categoryRepository.GetAll();
            response.Should().NotBeEmpty();
            response.Count().Should().BeGreaterThanOrEqualTo(1);
        }

        [Fact]
        public void GetCategoryById_WhenCategoryIdIsPassed_ShouldReturnCategory()
        {
            var expectedCategory = new Category
            {
                Id = 1,
                CategoryName = "Fruits & Vegetables",
                Description = ""
            };
            var result =  _categoryRepository.GetById(1);
            result.Should().NotBeNull(); 
            result.Should().BeOfType<Category>();
            result.Id.Should().Be(expectedCategory.Id);
            result.CategoryName.Should().Be(expectedCategory.CategoryName);

        }
        [Fact]
        public void GetCategoryById_WhenCategoryIdNotExist_ShouldReturnNull()
        {
            var result = _categoryRepository.GetById(0);
            result.Should().BeNull();
        }

        [Fact]
        public void UpdateCategory_ShouldUpdateCategorySuccessfully()
        {

            var categoryToUpdate = _categoryRepository.GetById(1);
            categoryToUpdate.CategoryName = "Updated Fruits & Vegetables";
            categoryToUpdate.Description = "Updated Description";

            _categoryRepository.Update(categoryToUpdate);
            var updatedCategory = _categoryRepository.GetById(1);

            updatedCategory.Should().NotBeNull();
            updatedCategory.CategoryName.Should().Be("Updated Fruits & Vegetables");
            updatedCategory.Description.Should().Be("Updated Description");
        }

        [Fact]
        public void DeleteCategory_ShouldRemoveCategorySuccessfully()
        {
            var categoryIdToDelete = 2;
            var isDeleted = _categoryRepository.Delete(categoryIdToDelete);
            isDeleted.Should().Be(true);
        }
    }
}
