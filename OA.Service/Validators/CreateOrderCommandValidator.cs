using ECom.Application.Features.OrderFeatures.Commands;
using FluentValidation;

namespace ECom.Application.Validators
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotEmpty().WithMessage("Customer id cannot be empty")
                .GreaterThan(0).WithMessage("Customer id is non negative");
            RuleFor(x => x.OrderDate)
                .NotEmpty().WithMessage("Order date cannot be empty");
        }
    }
}