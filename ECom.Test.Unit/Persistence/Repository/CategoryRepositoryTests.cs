
using ECom.Domain.Entities;
using ECom.Infrastructure.Repository;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
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
        public void AddCategory_WhenShouldAddNewCategory()
        {
            var newCategory = new Category { Id = 13, CategoryName = "Clothing", Description = "Apparel items" };
            var response = _categoryRepository.AddCategory(newCategory);
            response.Should().NotBeNull();
            response.Result.CategoryName.Should().Be("Clothing");
            response.Result.Description.Should().Be("Apparel items");
        }

        [Fact]
        public async Task GetAllCategories_ShouldReturnListOfCategories()
        {
            var response = await _categoryRepository.GetAllCategories();
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
            var result = _categoryRepository.GetCategoryById(1);
            result.Should().NotBeNull();
            //result.Should().BeOfType<Category>();
            result.Id.Should().Be(expectedCategory.Id);
            result.Result.CategoryName.Should().Be(expectedCategory.CategoryName);
        }

        [Fact]
        public void GetCategoryById_WhenCategoryIdNotExist_ShouldReturnNull()
        {
            var result = _categoryRepository.GetCategoryById(0);
            result.Should().BeNull();
        }

        [Fact]
        public void UpdateCategory_ShouldUpdateCategorySuccessfully()
        {
            var categoryToUpdate = _categoryRepository.GetCategoryById(1);
            categoryToUpdate.Result.CategoryName = "Updated Fruits & Vegetables";
            categoryToUpdate.Result.Description = "Updated Description";

            _categoryRepository.UpdateCategory(categoryToUpdate.Result);
            var updatedCategory = _categoryRepository.GetCategoryById(1);

            updatedCategory.Should().NotBeNull();
            updatedCategory.Result.CategoryName.Should().Be("Updated Fruits & Vegetables");
            updatedCategory.Result.Description.Should().Be("Updated Description");
        }

        [Fact]
        public void DeleteCategory_ShouldRemoveCategorySuccessfully()
        {
            var categoryIdToDelete = 2;
            var idDeleted = _categoryRepository.DeleteCategory(categoryIdToDelete);
            idDeleted.Should().Be(2);
        }
    }
}