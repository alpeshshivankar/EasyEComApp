using AutoMapper;
using ECom.Application.Persistance;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ECom.Application.Features.CategoryFeatures.Commands
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, int>
    {
        private readonly InMemoryDbContext _context;
        private readonly IMapper _mapper;

        public UpdateCategoryCommandHandler(InMemoryDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = _context.Categories.Where(a => a.Id == request.Id).FirstOrDefault();

            if (category == null) return default;

            _mapper.Map(request, category);
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            return category.Id;
        }
    }
}