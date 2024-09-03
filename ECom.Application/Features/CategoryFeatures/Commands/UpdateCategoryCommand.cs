using ECom.Application.DTOs;
using ECom.Application.Models;
using MediatR;

namespace ECom.Application.Features.CategoryFeatures.Commands
{
    public class UpdateCategoryCommand :  IRequest<Result<CategoryDto>>
    {
        public int Id { get; }
        public CategoryDto CategoryDto { get; }

        public UpdateCategoryCommand(int id, CategoryDto categoryDto)
        {
            Id = id;
            CategoryDto = categoryDto;
        }
    }
}