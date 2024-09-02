using AutoFixture;
using AutoMapper;
using ECom.Domain.Contract;
using ECom.Persistence.CategoryRepository;
using ECom.Persistence.CustomerRepository;
using ECom.Persistence.OrderRepository;
using ECom.Persistence.ProductRepository;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Moq;

namespace ECom.Test.Unit
{
    public class DbContextTest
    {
        public Fixture Fixture;
        public Mock<ECom.Persistence.CustomerRepository.ICustomerRepository> MoqCustomerReposistory;
        public Mock<ECom.Persistence.CategoryRepository.ICategoryRepository> MoqCategoryRepository;
        public Mock<ECom.Persistence.ProductRepository.IProductRepository> MoqProductReposistory;
        public Mock<ECom.Persistence.OrderRepository.IOrderRepository> MoqOrderReposistory;

        public Mock<Domain.Contract.ICustomerRepository> MoqCustomerService;
        public Mock<Domain.Contract.ICategoryRepository> MoqCategoryService;
        public Mock<Domain.Contract.IProductRepository> MoqProductService;
        public Mock<Domain.Contract.IOrderRepository> MoqOrderService;

        public Mock<IMapper> MoqMapper;
        public Mock<IValidator> MoqValidator;
        public Mock<HttpResponse> HttpResponseMock;

        public DbContextTest()
        {
            Fixture = new Fixture();
            MoqCustomerReposistory = new Mock<ECom.Persistence.CustomerRepository.ICustomerRepository>();
            MoqCategoryRepository = new Mock<ECom.Persistence.CategoryRepository.ICategoryRepository>();
            MoqProductReposistory = new Mock<ECom.Persistence.ProductRepository.IProductRepository>();
            MoqOrderReposistory = new Mock<ECom.Persistence.OrderRepository.IOrderRepository>();
            MoqCustomerService = new Mock<Domain.Contract.ICustomerRepository> { CallBase = true };
            MoqCategoryService = new Mock<Domain.Contract.ICategoryRepository>();
            MoqProductService = new Mock<Domain.Contract.IProductRepository>();
            MoqOrderService = new Mock<Domain.Contract.IOrderRepository>();

            MoqMapper = new Mock<IMapper>();
            MoqValidator = new Mock<IValidator>();
            HttpResponseMock = new Mock<HttpResponse>();
        }
    }
}
