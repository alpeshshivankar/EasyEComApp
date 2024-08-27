using MediatR;
using Microsoft.EntityFrameworkCore;
using ECom.Domain.Entities;
using ECom.Persistence;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ECom.Service.Features.ProductFeatures.Queries
{
    public class GetAllProductQuery : IRequest<IEnumerable<Product>>
    {

        public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQuery, IEnumerable<Product>>
        {
            private readonly IApplicationDbContext _context;
            public GetAllProductQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<IEnumerable<Product>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
            {
                var productList = await _context.Products.ToListAsync();
                if (productList == null)
                {
                    return null;
                }
                return productList.AsReadOnly();
            }

        }
    }
}
