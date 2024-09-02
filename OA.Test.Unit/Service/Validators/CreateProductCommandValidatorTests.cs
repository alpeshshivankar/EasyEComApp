using ECom.Application.Features.ProductFeatures.Commands;
using ECom.Application.Validators;
using FluentValidation.TestHelper;
using Xunit;

namespace ECom.Test.Unit.Service.Validators
{
    public class CreateProductCommandValidatorTests
    {
        private readonly CreateProductCommandValidator _validator;

        public CreateProductCommandValidatorTests()
        {
            _validator = new CreateProductCommandValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Name_Is_Empty()
        {
            var command = new CreateProductCommand { ProductName = "", UnitPrice = 100, CategoryId = 1 };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.ProductName);
        }

        [Fact]
        public void Should_Have_Error_When_Price_Is_Zero()
        {
            var command = new CreateProductCommand { ProductName = "Test Product", UnitPrice = 0, CategoryId = 1 };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.UnitPrice);
        }
    }
}