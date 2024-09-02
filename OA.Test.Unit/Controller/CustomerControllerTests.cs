using ECom.Application.Features.CustomerFeatures.Queries;
using ECom.Controllers;
using ECom.Domain.Entities;
using ECom.Service.Features.CustomerFeatures.Commands;
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
    public class CustomerControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly CustomerController _controller;

        public CustomerControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new CustomerController(_mediatorMock.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnOkResult_WithListOfCategories()
        {
            var categories = new List<Customer>
            {
                new Customer {
                    Id=1,
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
                new Customer {
                    Id=2,
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
                }
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllCustomerQuery>(), default))
                .ReturnsAsync(categories);
            var result = await _controller.GetAll();
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(categories);
        }
        [Fact]
        public async Task GetById_ShouldReturnOkResult_WithCustomer()
        {
            var category = new Customer
            {
                Id=1,
                CustomerName = "Pattrick",
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
            };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetCustomerByIdQuery>(), default))
                .ReturnsAsync(category);
            var result = await _controller.GetById(1);
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(category);
        }

        [Fact]
        public async Task GetById_ShouldReturnNotFound_WhenCustomerDoesNotExist()
        {
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetCustomerByIdQuery>(), default))
                .ReturnsAsync((Customer)null);
            var result = await _controller.GetById(1);

            var notFoundResult = result as OkObjectResult;
            notFoundResult.Should().NotBeNull();
            notFoundResult.Value.Should().BeNull();
            notFoundResult.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Create_ShouldReturnCreatedAtActionResult_WhenCustomerIsCreated()
        {

            var command = new CreateCustomerCommand
            {
                CustomerName = "Pattrick",
                ContactName = "Pattrick DeCosta",
                ContactTitle = "Mr",
                Address = "11, Welington Street",
                City = "London",
                Region = "Liverpool",
                PostalCode = "L18 3HS",
                Country = "UK",
                Phone = "589774455",
                Fax = string.Empty,
                
            };
            var createdCustomer = new Customer
            {
                Id=3,
                CustomerName = "Pattrick",
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
            };
            _mediatorMock.Setup(m => m.Send(command, default)).ReturnsAsync(createdCustomer.Id);
            var result = await _controller.Create(command);
            var createdResult = result as OkObjectResult;
            createdResult.Should().NotBeNull();
            createdResult.StatusCode.Should().Be(200);
            createdResult.Value.Should().Be(createdCustomer.Id);

        }
        [Fact]
        public async Task Update_ShouldReturnNoContentResult_WhenCustomerIsUpdated()
        {
            var command = new UpdateCustomerCommand
            {
                Id = 1,
                CustomerName = "Pattrick",
                ContactName = "Pattrick DeCosta",
                ContactTitle = "Mr",
                Address = "11, Welington Street",
                City = "London",
                Region = "Liverpool",
                PostalCode = "L18 3HS",
                Country = "UK",
                Phone = "589774455",
                Fax = string.Empty,

            };
            _mediatorMock.Setup(m => m.Send(command, default))
                .ReturnsAsync(command.Id);
            var result = await _controller.Update(command.Id,command);
            var noContentResult = result as OkObjectResult;
            noContentResult.Should().NotBeNull();
            noContentResult.StatusCode.Should().Be(200);
        }
        [Fact]
        public async Task Delete_ShouldReturnNoContentResult_WhenCustomerIsDeleted()
        {
            var command = new DeleteCustomerCommand { Id = 1 };
            _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteCustomerCommand>(), default))
                .ReturnsAsync(command.Id);
            var result = await _controller.Delete(command.Id);
            var noContentResult = result as OkObjectResult;
            noContentResult.Should().NotBeNull();
            noContentResult.StatusCode.Should().Be(200);
            noContentResult.Value.Should().Be(command.Id);
        }
    }
}
