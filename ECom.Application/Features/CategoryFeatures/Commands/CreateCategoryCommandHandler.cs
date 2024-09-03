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
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Result<CategoryDto>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<Result<CategoryDto>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<Category>(request.CategoryDto);
            await _categoryRepository.AddAsync(category);
            var categoryDto = _mapper.Map<CategoryDto>(category);
            return Result<CategoryDto>.Success(categoryDto);
        }
    }
}