using ECom.Domain.Entities;
using ECom.Persistence.ProductRepository;
using ECom.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

namespace ECom.Test.Unit.Persistence.Repository
{
    public class ProductRepositoryTests
    {
        private readonly DbContextOptionsBuilder<InMemoryDbContext> dbContext;
        private readonly InMemoryDbContext _imDbContext;
        private readonly ProductRepository _productRepository;
        public ProductRepositoryTests()
        {
            dbContext = new DbContextOptionsBuilder<InMemoryDbContext>();
            dbContext.UseInMemoryDatabase(Guid.NewGuid().ToString());
            _imDbContext = new InMemoryDbContext(dbContext.Options);
            _productRepository = new ProductRepository(_imDbContext);

        }
        [Fact]
        public void AddProduct_ShouldAddNewProduct()
        {
            var newProduct = new Product { Id = 15, ProductName = "Banana", CategoryId = 1, UnitPrice = 4.4M };
            var response = _productRepository.Add(newProduct);
            var result = _productRepository.GetById(15);
            result.Should().NotBeNull();
            result.ProductName.Should().Be("Banana");
            result.CategoryId.Should().Be(1);
            result.UnitPrice.Should().Be(4.4M);
        }
        [Fact]
        public async Task GetAllProducts_ShouldReturnListOfProducts()
        {
            var response = await _productRepository.GetAll();
            response.Should().NotBeEmpty();
            response.Count().Should().BeGreaterThanOrEqualTo(1);
        }

        [Fact]
        public void GetProductById_WhenProductIdIsPassed_ShouldReturnProduct()
        {
            var expectedProduct = new Product
            { 
                Id = 1, 
                ProductName = "Apple", 
                CategoryId = 1, 
                UnitPrice = 1.1M 
            };
            var result = _productRepository.GetById(1);
            result.Should().NotBeNull();
            result.Should().BeOfType<Product>();
            result.Id.Should().Be(expectedProduct.Id);
            result.ProductName.Should().Be(expectedProduct.ProductName);

        }
        [Fact]
        public void GetProductById_WhenProductIdNotExist_ShouldReturnNull()
        {
            var result = _productRepository.GetById(0);
            result.Should().BeNull();
        }

        [Fact]
        public void UpdateProduct_ShouldUpdateProductSuccessfully()
        {

            var productToUpdate = _productRepository.GetById(1);
            productToUpdate.ProductName = "Updated Apple";
            productToUpdate.UnitPrice = 4.4M;

            _productRepository.Update(productToUpdate);
            var updatedProduct = _productRepository.GetById(1);

            updatedProduct.Should().NotBeNull();
            updatedProduct.ProductName.Should().Be("Updated Apple");
            updatedProduct.UnitPrice.Should().Be(4.4M);
        }

        [Fact]
        public void DeleteProduct_ShouldRemoveProductSuccessfully()
        {
            var productIdToDelete = 2;
            var isDeleted = _productRepository.Delete(productIdToDelete);
            isDeleted.Should().Be(true);
        }
    }
}
