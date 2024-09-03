using AutoMapper;
using ECom.Application.Features.ProductFeatures.Commands;
using ECom.Application.Mapping;

using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ECom.Test.Unit.Service.Feature.Product.Commands
{
    public class CreateProductCommandHandlerTest
    {
        private InMemoryDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<InMemoryDbContext>()
                            .UseInMemoryDatabase(databaseName: "TestDatabase")
                            .Options;
            var dbContext = new InMemoryDbContext(options);

            return dbContext;
        }

        private IMapper GetMapper()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperProfile>();
            });

            return configuration.CreateMapper();
        }

        [Fact]
        public async Task Handle_ShouldAddProductToDatabase()
        {
            var dbContext = GetInMemoryDbContext();
            var mapper = GetMapper();
            var handler = new CreateProductCommandHandler(dbContext, mapper);
            var command = new CreateProductCommand
            {
                ProductName = "Electronics",
                UnitPrice = 2.2M,
                CategoryId = 1
            };
            await handler.Handle(command, CancellationToken.None);
            var categoryFromDb = await dbContext.Products.FirstOrDefaultAsync(c => c.ProductName == "Electronics");
            categoryFromDb.Should().NotBeNull();
        }
    }
}