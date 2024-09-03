using AutoMapper;
using ECom.Application.DTOs;
using ECom.Application.Features.CategoryFeatures.Commands;
using ECom.Application.Mapping;

using ECom.Test.Unit.Fixtures;
using FluentAssertions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ECom.Test.Unit.Application.Feature.Category.Commands
{
    public class CreateCategoryCommandHandlerTest : IClassFixture<CategoryRepositoryFixture>
    {
        private readonly CategoryRepositoryFixture _fixture;
        private readonly CreateCategoryCommandHandler _handler;
        public Mock<IMapper> MoqMapper;

        public CreateCategoryCommandHandlerTest(CategoryRepositoryFixture fixture)
        {
            _fixture = fixture;
            MoqMapper = new Mock<IMapper>();
            _handler = new CreateCategoryCommandHandler(_fixture.MockCategoryRepository.Object,MoqMapper.Object);
        }

        [Fact]
        public async Task Handle_ValidCommand_AddsCategoryAndReturnsId()
        {
            CategoryDto categoryDto = new CategoryDto { CategoryName = "Test", Description=""};
            var command = new CreateCategoryCommand(categoryDto);

            var result = await _handler.Handle(command, CancellationToken.None);

            result.Should().NotBeNull();
            result.Should().Be(result.Value.Id > 0);
            result.Value.CategoryName.Should().Be("Test");
            _fixture.MockCategoryRepository.Verify(repo => repo.AddCategory(It.IsAny<Domain.Entities.Category>()), Times.Once);
        }

        [Fact]
        public async Task Handle_NullCommand_ThrowsArgumentNullException()
        {
            CreateCategoryCommand command = null;
            await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_EmptyCategoryName_ThrowsArgumentException()
        {
            CategoryDto categoryDto = new CategoryDto { CategoryName = "", Description = "" };
            var command = new CreateCategoryCommand(categoryDto);
            await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}
