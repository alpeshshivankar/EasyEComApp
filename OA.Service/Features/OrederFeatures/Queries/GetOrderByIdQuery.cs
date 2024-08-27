using ECom.Domain.Entities;
using ECom.Persistence;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ECom.Service.Features.OrederFeatures.Queries
{
    public  class GetOrderByIdQuery:IRequest<Order>
    {
        public int Id { get; set; }
        public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, Order>
        {
            private readonly IApplicationDbContext _context;
            public GetOrderByIdQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<Order> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
            {
                var order = _context.Orders.Where(a => a.Id == request.Id).FirstOrDefault();
                if (order == null) return null;
                return order;
            }
        }
    }
}
