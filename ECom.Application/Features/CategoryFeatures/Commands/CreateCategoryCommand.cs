using ECom.Application.DTOs;
using ECom.Application.Models;
using MediatR;

namespace ECom.Application.Features.CategoryFeatures.Commands
{
    public class CreateCategoryCommand : IRequest<Result<CategoryDto>>
    {
        public CategoryDto CategoryDto { get; }

        public CreateCategoryCommand(CategoryDto categoryDto)
        {
            CategoryDto = categoryDto;
        }

    }
}
