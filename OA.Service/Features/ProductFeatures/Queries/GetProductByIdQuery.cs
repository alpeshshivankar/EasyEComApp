using MediatR;
using ECom.Domain.Entities;
using ECom.Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ECom.Service.Features.ProductFeatures.Queries;

namespace ECom.Service.Features.ProductFeatures.Queries
{
    public class GetProductByIdQuery : IRequest<Product>
    {
        public int Id { get; set; }
        public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product>
        {
            private readonly IApplicationDbContext _context;
            public GetProductByIdQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
            {
                var product = _context.Products.Where(a => a.Id == request.Id).FirstOrDefault();
                if (product == null) return null;
                return product;
            }
        }
    }
}
