using ECom.Domain.Entities;
using ECom.Persistence.CustomerRepository;
using ECom.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

namespace ECom.Test.Unit.Persistence.Repository
{
    public class CustomerRepositoryTests
    {
        private readonly DbContextOptionsBuilder<InMemoryDbContext> dbContext;
        private readonly InMemoryDbContext _imDbContext;
        private readonly CustomerRepository _customerRepository;
        public CustomerRepositoryTests()
        {
            dbContext = new DbContextOptionsBuilder<InMemoryDbContext>();
            dbContext.UseInMemoryDatabase(Guid.NewGuid().ToString());
            _imDbContext = new InMemoryDbContext(dbContext.Options);
            _customerRepository = new CustomerRepository(_imDbContext);

        }
        [Fact]
        public void AddCategory_ShouldAddNewCategory()
        {
            var newCustomer = new Customer
            {
                Id = 3,
                CustomerName = "Tony",
                ContactName = "Tony Stark",
                ContactTitle = "Mr",
                Address = "135,Stark Tower",
                City = "NewYork",
                Region = "Lancaster",
                PostalCode = "LA1 1AT",
                Country = "India",
                Phone = "815d8781124",
                Fax = string.Empty,
                CreatedBy = 1002,
                CreatedDate = DateTime.Now.AddDays(-5).Date
            };
            var response = _customerRepository.Add(newCustomer);

            response.Should().NotBeNull();
            response.CustomerName.Should().Be("Tony");
            response.City.Should().Be("NewYork");
        }
        [Fact]
        public async Task GetAllCustomers_ShouldReturnListOfCustomers()
        {
            var response = await _customerRepository.GetAll();
            response.Should().NotBeEmpty();
            response.Count().Should().BeGreaterThanOrEqualTo(1);
        }

        [Fact]
        public void GetCustomerById_WhenCustomerIdIsPassed_ShouldReturnCustomer()
        {
            var expectedCustomer = new Customer
            {
                Id = 2,
                CustomerName = "Robert",
                ContactName = "Robert",
                ContactTitle = "Mr",
                Address = "135,Churchil Street",
                City = "vrindavan",
                Region = "Lancaster",
                PostalCode = "LA1 1AT",
                Country = "India",
                Phone = "815d8781124",
                Fax = string.Empty,
                CreatedBy = 1002,
                CreatedDate = DateTime.Now.AddDays(-5).Date
            };
            var result = _customerRepository.GetById(2);
            result.Should().NotBeNull();
            result.Should().BeOfType<Customer>();
            result.Id.Should().Be(expectedCustomer.Id);
            result.CustomerName.Should().Be(expectedCustomer.CustomerName);

        }
        [Fact]
        public void GetCustomerById_WhenCustomerIdNotExist_ShouldReturnNull()
        {
            var result = _customerRepository.GetById(0);
            result.Should().BeNull();
        }

        [Fact]
        public void UpdateCustomer_ShouldUpdateCustomerSuccessfully()
        {

            var customerToUpdate = _customerRepository.GetById(1);
            customerToUpdate.CustomerName = "Roger David";
            customerToUpdate.Address = "Amroli street";

            _customerRepository.Update(customerToUpdate);
            var updatedCustomer = _customerRepository.GetById(1);

            updatedCustomer.Should().NotBeNull();
            updatedCustomer.CustomerName.Should().Be("Roger David");
            updatedCustomer.Address.Should().Be("Amroli street");
        }

        [Fact]
        public void DeleteCustomer_ShouldRemoveCustomerSuccessfully()
        {
            var customerIdToDelete = 2;
            var isDeleted = _customerRepository.Delete(customerIdToDelete);
            isDeleted.Should().Be(true);
        }
    }
}
