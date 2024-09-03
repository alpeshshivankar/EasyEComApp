using AutoFixture;
using AutoMapper;

using ECom.Domain.Contract;
using ECom.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;

namespace ECom.Test.Unit.Fixtures
{
    public class ApplicationFixture
    {
        public Fixture Fixture;
        public DbContextOptionsBuilder DbContextOptionsBuilder;
        public Mock<IMapper> MoqMapper;
        public Mock<IValidator> MoqValidator;
        public Mock<HttpResponse> HttpResponseMock;
        public Mock<HttpContext> HttpContextMock;
        public Mock<InMemoryDbContext> moqInMemoryDbContext;
        public Mock<ICustomerRepository> MoqCustomerRepository;
        public Mock<ICategoryRepository> MoqCategoryRepository;
        public Mock<IProductRepository> MoqProductRepository;
        public Mock<IOrderRepository> MoqOrderRepository;
        public ApplicationFixture()
        {
            Fixture = new Fixture();
            moqInMemoryDbContext = new Mock<InMemoryDbContext>();
            MoqMapper = new Mock<IMapper>();
            HttpResponseMock = new Mock<HttpResponse>();
            HttpContextMock = new Mock<HttpContext>();
            DbContextOptionsBuilder = new DbContextOptionsBuilder<InMemoryDbContext>();
            DbContextOptionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            MoqCustomerRepository= new Mock<ICustomerRepository>() { CallBase = true };
            MoqCategoryRepository = new Mock<ICategoryRepository>() { CallBase = true };
            MoqProductRepository = new Mock<IProductRepository>() { CallBase = true };
            MoqOrderRepository = new Mock<IOrderRepository>() { CallBase = true };
        }
        
    }
}