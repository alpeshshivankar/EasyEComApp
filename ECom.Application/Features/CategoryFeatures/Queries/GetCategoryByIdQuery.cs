using ECom.Domain.Contract;
using ECom.Domain.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ECom.Application.Features.CategoryFeatures.Queries
{
    public class GetCategoryByIdQuery : IRequest<Category>
    {
        public int Id { get; set; }

        public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Category>
        {
            private readonly ICategoryRepository _context;

            public GetCategoryByIdQueryHandler(ICategoryRepository context)
            {
                _context = context;
            }

            public async Task<Category> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
            {
                var category = await _context.GetByIdAsync(request.Id);
                if (category == null) return null;
                return category;
            }
        }
    }
}