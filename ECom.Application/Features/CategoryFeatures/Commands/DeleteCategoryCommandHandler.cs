using AutoMapper;
using ECom.Application.DTOs;
using ECom.Domain.Contract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECom.Application.Features.CategoryFeatures.Commands
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Result<int>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(request.Id);
            if (category == null)
            {
                return Result<int>.Failure("Category not found.");
            }
            await _categoryRepository.DeleteAsync(category.Id);
            return Result<int>.Success(category.Id);
        }
    }
}