using AutoMapper;
using ECom.Application.DTOs;
using ECom.Application.Models;
using ECom.Domain.Contract;
using ECom.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECom.Application.Features.CategoryFeatures.Commands
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Result<CategoryDto>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<Result<CategoryDto>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(request.Id);
            if (category == null)
            {
                return Result<CategoryDto>.Failure("Category not found.");
            }
            var categoryDto = _mapper.Map<CategoryDto>(category);
            await _categoryRepository.UpdateAsync(category);
            var categoryResult = _mapper.Map<Category>(categoryDto);
            return Result<CategoryDto>.Success(categoryDto);
        }
    }
}