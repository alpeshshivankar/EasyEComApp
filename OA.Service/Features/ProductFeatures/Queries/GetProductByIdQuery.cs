using MediatR;
using ECom.Domain.Entities;
using ECom.Application.Persistance;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ECom.Application.Features.ProductFeatures.Queries
{
    public class GetProductByIdQuery : IRequest<Product>
    {
        public int Id { get; set; }

        public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product>
        {
            private readonly InMemoryDbContext _context;

            public GetProductByIdQueryHandler(InMemoryDbContext context)
            {
                _context = context;
            }

            public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
            {
                var product = await _context.Products.Where(a => a.Id == request.Id).FirstOrDefaultAsync();
                if (product == null) return null;
                return product;
            }
        }
    }
}