
using ECom.Domain.Entities;
using ECom.Infrastructure.Repository;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ECom.Test.Unit.Persistence.Repository
{
    public class OrderRepositoryTests
    {
        private readonly DbContextOptionsBuilder<InMemoryDbContext> dbContext;
        private readonly InMemoryDbContext _imDbContext;
        private readonly OrderRepository _orderRepository;

        public OrderRepositoryTests()
        {
            dbContext = new DbContextOptionsBuilder<InMemoryDbContext>();
            dbContext.UseInMemoryDatabase(Guid.NewGuid().ToString());
            _imDbContext = new InMemoryDbContext(dbContext.Options);
            _orderRepository = new OrderRepository(_imDbContext);
        }

        [Fact]
        public void AddOrder_ShouldAddNewOrder()
        {
            var newOrder = new Order
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
                            Id= 11,
                            ProductName = "Cake",
                            UnitPrice= 2.2M,
                            CategoryId = 4
                        }
                    },
                CreatedDate = DateTime.Now.Date,
            };
            var response = _orderRepository.AddOrder(newOrder);
            var result = _orderRepository.GetOrderById(response.Id);
            response.Should().NotBeNull();
            response.OrderStatus.Should().Be("Open");
            response.ProductDetails.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task GetAllOrders_ShouldReturnListOfOrders()
        {
            var response = await _orderRepository.GetAllOrders();
            response.Should().NotBeEmpty();
            response.Count().Should().BeGreaterThanOrEqualTo(1);
        }

        [Fact]
        public void GetOrderById_WhenOrderIdIsPassed_ShouldReturnOrder()
        {
            var expectedOrder = new Order
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
                            Id= 11,
                            ProductName = "Cake",
                            UnitPrice= 2.2M,
                            CategoryId = 4
                        }
                    },
                CreatedDate = DateTime.Now.Date,
            };
            var result = _orderRepository.GetOrderById(1);
            result.Should().NotBeNull();
            result.Should().BeOfType<Order>();
            result.Id.Should().Be(expectedOrder.Id);
        }

        [Fact]
        public void GetOrderById_WhenOrderIdNotExist_ShouldReturnNull()
        {
            var result = _orderRepository.GetOrderById(0);
            result.Should().BeNull();
        }

        [Fact]
        public void UpdateOrder_ShouldUpdateOrderSuccessfully()
        {
            var orderToUpdate = _orderRepository.GetOrderById(1);
            orderToUpdate.OrderDate = DateTime.Now.Date;
            _orderRepository.Update(orderToUpdate);
            var updatedOrder = _orderRepository.GetOrderById(1);
            updatedOrder.Should().NotBeNull();
            updatedOrder.OrderDate.Should().Be(DateTime.Now.Date);
        }

        [Fact]
        public void DeleteOrder_ShouldRemoveOrderSuccessfully()
        {
            var orderIdToDelete = 2;
            var isDeleted = _orderRepository.Delete(orderIdToDelete);
            isDeleted.Should().Be(true);
        }
    }
}