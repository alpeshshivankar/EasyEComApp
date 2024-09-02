using AutoMapper;
using ECom.Application.Features.ProductFeatures.Queries;
using ECom.Application.Mapping;
using ECom.Domain.Contract;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using static ECom.Application.Features.ProductFeatures.Queries.GetAllProductQuery;

namespace ECom.Test.Unit.Service.Feature.Product.Queries
{
    public class GetAllProductQueryHandlerTest
    {

        private IMapper GetMapper()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperProfile>();
            });
            return configuration.CreateMapper();
        }

        [Fact]
        public async Task Handle_ShouldReturnAllProducts()
        {
            var mockProductService = new Mock<IProductRepository>();
            var mapper = GetMapper();

            var categories = new List<Domain.Entities.Product>
            {
                new Domain.Entities.Product { Id = 1, ProductName = "Electronics", UnitPrice=2.2M, CategoryId=1 },
                new Domain.Entities.Product { Id = 2, ProductName = "Groceries", UnitPrice=2.2M, CategoryId=2  }
            };

            mockProductService.Setup(service => service.GetAllProducts())
                               .ReturnsAsync(categories);

            var handler = new GetAllProductQueryHandler(mockProductService.Object);
            var query = new GetAllProductQuery();

            var result = await handler.Handle(query, CancellationToken.None);
            result.Should().HaveCount(2);
            result.Should().AllSatisfy(category =>
            {
                category.Id.Should().BeGreaterThan(0);
                category.ProductName.Should().NotBeNullOrWhiteSpace();
            });
        }
    }
}
