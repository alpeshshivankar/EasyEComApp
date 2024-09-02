using AutoMapper;
using ECom.Application.Features.CategoryFeatures.Commands;
using ECom.Application.Mapping;
using ECom.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ECom.Test.Unit.Service.Feature.Category.Commands
{
    public class CreateCategoryCommandHandlerTest
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
        public async Task Handle_ShouldAddCategoryToDatabase()
        {
            var dbContext = GetInMemoryDbContext();
            var mapper = GetMapper();
            var handler = new CreateCategoryCommandHandler(dbContext, mapper);
            var command = new CreateCategoryCommand
            {
                CategoryName = "Electronics",
                Description = "Category for electronic products."
            };
            await handler.Handle(command, CancellationToken.None);
            var categoryFromDb = await dbContext.Categories.FirstOrDefaultAsync(c => c.CategoryName == "Electronics");
            Assert.NotNull(categoryFromDb);
            Assert.Equal("Electronics", categoryFromDb.CategoryName);
            Assert.Equal("Category for electronic products.", categoryFromDb.Description);
        }
    }
}