using MediatR;
using ECom.Domain.Entities;
using ECom.Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ECom.Service.Features.CustomerFeatures.Queries
{
    public class GetCategoryByIdQuery : IRequest<Category>
    {
        public int Id { get; set; }
        public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Category>
        {
            private readonly IApplicationDbContext _context;
            public GetCategoryByIdQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<Category> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
            {
                var category = _context.Categories.Where(a => a.Id == request.Id).FirstOrDefault();
                if (category == null) return null;
                return category;
            }

        }
    }
}
