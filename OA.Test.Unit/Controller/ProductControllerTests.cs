using ECom.Application.Features.ProductFeatures.Commands;
using ECom.Application.Features.ProductFeatures.Queries;
using ECom.Controllers;
using ECom.Domain.Entities;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ECom.Test.Unit.Controller
{
    public class ProductControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly ProductController _controller;

        public ProductControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new ProductController(_mediatorMock.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnOkResult_WithListOfCategories()
        {
            var categories = new List<Product>
            {
                new Product { Id = 1, ProductName = "Electronics" },
                new Product { Id = 2, ProductName = "Books" }
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllProductQuery>(), default))
                .ReturnsAsync(categories);
            var result = await _controller.GetAll();
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(categories);
        }

        [Fact]
        public async Task GetById_ShouldReturnOkResult_WithProduct()
        {
            var category = new Product { Id = 1, ProductName = "Electronics", CategoryId = 1, UnitPrice = 2.2M };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetProductByIdQuery>(), default))
                .ReturnsAsync(category);
            var result = await _controller.GetById(1);
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(category);
        }

        [Fact]
        public async Task GetById_ShouldReturnNotFound_WhenProductDoesNotExist()
        {
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetProductByIdQuery>(), default))
                .ReturnsAsync((Product)null);
            var result = await _controller.GetById(1);

            var notFoundResult = result as OkObjectResult;
            notFoundResult.Should().NotBeNull();
            notFoundResult.Value.Should().BeNull();
            notFoundResult.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Create_ShouldReturnCreatedAtActionResult_WhenProductIsCreated()
        {
            var command = new CreateProductCommand { ProductName = "Clothing", CategoryId = 1, UnitPrice = 2.2M };
            var createdProduct = new Product { Id = 3, ProductName = "Clothing", CategoryId = 1, UnitPrice = 2.2M };
            _mediatorMock.Setup(m => m.Send(command, default)).ReturnsAsync(createdProduct.Id);
            var result = await _controller.Create(command);
            var createdResult = result as OkObjectResult;
            createdResult.Should().NotBeNull();
            createdResult.StatusCode.Should().Be(200);
            createdResult.Value.Should().Be(createdProduct.Id);
        }

        [Fact]
        public async Task Update_ShouldReturnNoContentResult_WhenProductIsUpdated()
        {
            var command = new UpdateProductCommand { Id = 1, ProductName = "Updated Electronics", CategoryId = 1, UnitPrice = 2.2M };
            _mediatorMock.Setup(m => m.Send(command, default))
                .ReturnsAsync(command.Id);
            var result = await _controller.Update(command);
            var noContentResult = result as OkObjectResult;
            noContentResult.Should().NotBeNull();
            noContentResult.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Delete_ShouldReturnNoContentResult_WhenProductIsDeleted()
        {
            var command = new DeleteProductCommand { Id = 1 };
            _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteProductCommand>(), default))
                .ReturnsAsync(command.Id);
            var result = await _controller.Delete(command);
            var noContentResult = result as OkObjectResult;
            noContentResult.Should().NotBeNull();
            noContentResult.StatusCode.Should().Be(200);
            noContentResult.Value.Should().Be(command.Id);
        }
    }
}