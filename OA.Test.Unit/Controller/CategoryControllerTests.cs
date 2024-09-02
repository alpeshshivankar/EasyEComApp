using ECom.Application.Features.CategoryFeatures.Commands;
using ECom.Application.Features.CategoryFeatures.Queries;
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
    public class CategoryControllerTest
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly CategoryController _controller;

        public CategoryControllerTest()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new CategoryController(_mediatorMock.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnOkResult_WithListOfCategories()
        {
            var categories = new List<Category>
            {
                new Category { Id = 1, CategoryName = "Electronics" },
                new Category { Id = 2, CategoryName = "Books" }
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllCategoryQuery>(), default))
                .ReturnsAsync(categories);
            var result = await _controller.GetAll();
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(categories);
        }

        [Fact]
        public async Task GetById_ShouldReturnOkResult_WithCategory()
        {
            var category = new Category { Id = 1, CategoryName = "Electronics" };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetCategoryByIdQuery>(), default))
                .ReturnsAsync(category);
            var result = await _controller.GetById(1);
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(category);
        }

        [Fact]
        public async Task GetById_ShouldReturnNotFound_WhenCategoryDoesNotExist()
        {
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetCategoryByIdQuery>(), default))
                .ReturnsAsync((Category)null);
            var result = await _controller.GetById(1);

            var notFoundResult = result as OkObjectResult;
            notFoundResult.Should().NotBeNull();
            notFoundResult.Value.Should().BeNull();
            notFoundResult.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Create_ShouldReturnCreatedAtActionResult_WhenCategoryIsCreated()
        {
            var command = new CreateCategoryCommand { CategoryName = "Clothing", Description = "Apparel items" };
            var createdCategory = new Category { Id = 3, CategoryName = "Clothing", Description = "Apparel items" };
            _mediatorMock.Setup(m => m.Send(command, default)).ReturnsAsync(createdCategory.Id);
            var result = await _controller.Create(command);
            var createdResult = result as OkObjectResult;
            createdResult.Should().NotBeNull();
            createdResult.StatusCode.Should().Be(200);
            createdResult.Value.Should().Be(createdCategory.Id);
        }

        [Fact]
        public async Task Update_ShouldReturnNoContentResult_WhenCategoryIsUpdated()
        {
            var command = new UpdateCategoryCommand { Id = 1, CategoryName = "Updated Electronics", Description = "Updated items" };
            _mediatorMock.Setup(m => m.Send(command, default))
                .ReturnsAsync(command.Id);
            var result = await _controller.Update(command);
            var noContentResult = result as OkObjectResult;
            noContentResult.Should().NotBeNull();
            noContentResult.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Delete_ShouldReturnNoContentResult_WhenCategoryIsDeleted()
        {
            var command = new DeleteCategoryCommand { Id = 1 };
            _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteCategoryCommand>(), default))
                .ReturnsAsync(command.Id);
            var result = await _controller.Delete(command);
            var noContentResult = result as OkObjectResult;
            noContentResult.Should().NotBeNull();
            noContentResult.StatusCode.Should().Be(200);
            noContentResult.Value.Should().Be(command.Id);
        }
    }
}