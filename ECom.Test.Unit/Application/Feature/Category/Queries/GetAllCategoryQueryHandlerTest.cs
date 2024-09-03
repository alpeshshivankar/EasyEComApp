using AutoMapper;
using ECom.Application.Features.CategoryFeatures.Queries;
using ECom.Application.Mapping;

using ECom.Domain.Contract;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using static ECom.Application.Features.CategoryFeatures.Queries.GetAllCategoryQuery;

namespace ECom.Test.Unit.Service.Feature.Category.Queries
{
    public class GetAllCategoryQueryHandlerTest
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
        public async Task Handle_ShouldReturnAllCategories()
        {
            var mockDbContext = new Mock<InMemoryDbContext>();
            var mockCategoryRepository = new Mock<ICategoryRepository>();
            var mapper = GetMapper();

            var categories = new List<Domain.Entities.Category>
            {
                new Domain.Entities.Category { Id = 1, CategoryName = "Electronics", Description = "Category for electronic products." },
                new Domain.Entities.Category { Id = 2, CategoryName = "Groceries", Description = "Category for grocery products." }
            };

            //mockDbContext.Setup(service => service.Categories.ToListAsync())
            //                  .ReturnsAsync(categories);

            var handler = new GetAllCategoryQueryHandler(mockCategoryRepository.Object);
            var query = new GetAllCategoryQuery();

            var result = await handler.Handle(query, CancellationToken.None);
            result.Should().HaveCount(2);
            result.Should().AllSatisfy(category =>
            {
                category.Id.Should().BeGreaterThan(0);
                category.CategoryName.Should().NotBeNullOrWhiteSpace();
            });
        }
    }
}