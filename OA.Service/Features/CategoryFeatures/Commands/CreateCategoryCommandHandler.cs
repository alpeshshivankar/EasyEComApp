using AutoMapper;
using ECom.Application.Persistance;
using ECom.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECom.Application.Features.CategoryFeatures.Commands
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
    {
        private readonly InMemoryDbContext _context;
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(InMemoryDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<Category>(request);
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category.Id;
        }
    }
}