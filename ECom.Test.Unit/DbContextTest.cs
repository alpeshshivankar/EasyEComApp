using AutoFixture;
using AutoMapper;
using ECom.Domain.Contract;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Moq;

namespace ECom.Test.Unit
{
    public class DbContextTest
    {
        public Fixture Fixture;
        public Mock<ICustomerRepository> MoqCustomerReposistory;
        public Mock<ICategoryRepository> MoqCategoryRepository;
        public Mock<IProductRepository> MoqProductReposistory;
        public Mock<IOrderRepository> MoqOrderReposistory;

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
            MoqCustomerReposistory = new Mock<ICustomerRepository>();
            MoqCategoryRepository = new Mock<ICategoryRepository>();
            MoqProductReposistory = new Mock<IProductRepository>();
            MoqOrderReposistory = new Mock<IOrderRepository>();
            MoqMapper = new Mock<IMapper>();
            MoqValidator = new Mock<IValidator>();
            HttpResponseMock = new Mock<HttpResponse>();
        }
    }
}