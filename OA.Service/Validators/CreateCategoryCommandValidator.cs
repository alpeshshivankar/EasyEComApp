using ECom.Domain.Entities;
using FluentValidation;

namespace ECom.Application.Validators
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.CategoryName)
                .NotEmpty().WithMessage("Category Name cannot be Empty")
                .MaximumLength(100).WithMessage("Category name cannot exceed the limit of 100 characters");
            RuleFor(category => category.Description)
            .MaximumLength(200).WithMessage("Description cannot be more than 200 characters.");
        }
    }
}