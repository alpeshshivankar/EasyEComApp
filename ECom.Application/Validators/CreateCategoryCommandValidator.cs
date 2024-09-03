using ECom.Application.Features.CategoryFeatures.Commands;
using FluentValidation;

namespace ECom.Application.Validators
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(x => x.CategoryDto.CategoryName).NotEmpty().WithMessage("Category name is required.");
        }
    }
}