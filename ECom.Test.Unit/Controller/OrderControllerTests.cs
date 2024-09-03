using ECom.Application.Features.OrderFeatures.Commands;
using ECom.Application.Features.OrderFeatures.Queries;
using ECom.Controllers;
using ECom.Domain.Entities;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ECom.Test.Unit.Controller
{
    public class OrderControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly OrderController _controller;

        public OrderControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new OrderController(_mediatorMock.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnOkResult_WithListOfCategories()
        {
            var categories = new List<Order>
            {
                new Order {
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
                new Order {
                    Id=2,
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
                }
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllOrderQuery>(), default))
                .ReturnsAsync(categories);
            var result = await _controller.GetAll();
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(categories);
        }

        [Fact]
        public async Task GetById_ShouldReturnOkResult_WithOrder()
        {
            var order = new Order
            {
                Id = 1,
                CustomerId = 1,
                OrderDate = DateTime.Now.AddDays(-3).Date,
                OrderFulfillmentDate = null,
                OrderStatus = "Open",
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
            };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetOrderByIdQuery>(), default))
                .ReturnsAsync(order);
            var result = await _controller.GetById(1);
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(order);
        }

        [Fact]
        public async Task GetById_ShouldReturnNotFound_WhenOrderDoesNotExist()
        {
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetOrderByIdQuery>(), default))
                .ReturnsAsync((Order)null);
            var result = await _controller.GetById(1);

            var notFoundResult = result as OkObjectResult;
            notFoundResult.Should().NotBeNull();
            notFoundResult.Value.Should().BeNull();
            notFoundResult.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Create_ShouldReturnCreatedAtActionResult_WhenOrderIsCreated()
        {
            var command = new CreateOrderCommand
            {
                CustomerId = 1,
                OrderDate = DateTime.Now.AddDays(-3).Date,
                OrderFulfillmentDate = null,
                OrderStatus = "Open",
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
            };
            var createdOrder = new Order
            {
                Id = 3,
                CustomerId = 1,
                OrderDate = DateTime.Now.AddDays(-3).Date,
                OrderFulfillmentDate = null,
                OrderStatus = "Open",
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
            };
            _mediatorMock.Setup(m => m.Send(command, default)).ReturnsAsync(createdOrder.Id);
            var result = await _controller.Create(command);
            var createdResult = result as OkObjectResult;
            createdResult.Should().NotBeNull();
            createdResult.StatusCode.Should().Be(200);
            createdResult.Value.Should().Be(createdOrder.Id);
        }

        [Fact]
        public async Task Update_ShouldReturnNoContentResult_WhenOrderIsUpdated()
        {
            var command = new UpdateOrderCommand
            {
                Id = 3,
                CustomerId = 1,
                OrderDate = DateTime.Now.AddDays(-3).Date,
                OrderFulfillmentDate = null,
                OrderStatus = "Open",
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
            };
            _mediatorMock.Setup(m => m.Send(command, default))
                .ReturnsAsync(command.Id);
            var result = await _controller.Update(command);
            var noContentResult = result as OkObjectResult;
            noContentResult.Should().NotBeNull();
            noContentResult.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Delete_ShouldReturnNoContentResult_WhenOrderIsDeleted()
        {
            var command = new DeleteOrderCommand { Id = 1 };
            _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteOrderCommand>(), default))
                .ReturnsAsync(command.Id);
            var result = await _controller.Delete(command);
            var noContentResult = result as OkObjectResult;
            noContentResult.Should().NotBeNull();
            noContentResult.StatusCode.Should().Be(200);
            noContentResult.Value.Should().Be(command.Id);
        }
    }
}