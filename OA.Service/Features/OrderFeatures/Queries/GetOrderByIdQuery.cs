using ECom.Domain.Entities;
using ECom.Application.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ECom.Application.Features.OrderFeatures.Queries
{
    public class GetOrderByIdQuery : IRequest<Order>
    {
        public int Id { get; set; }

        public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, Order>
        {
            private readonly InMemoryDbContext _context;

            public GetOrderByIdQueryHandler(InMemoryDbContext context)
            {
                _context = context;
            }

            public async Task<Order> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
            {
                var order = await _context.Orders.Where(a => a.Id == request.Id).FirstOrDefaultAsync();
                if (order == null) return null;
                return order;
            }
        }
    }
}