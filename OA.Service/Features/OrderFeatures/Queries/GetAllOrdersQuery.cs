using ECom.Domain.Entities;
using ECom.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ECom.Service.Features.OrderFeatures.Queries
{
    public class GetAllOrderQuery:IRequest<IEnumerable<Order>>
    {
        public class GetAllOrderQueryHandler : IRequestHandler<GetAllOrderQuery, IEnumerable<Order>>
        {
            private readonly IApplicationDbContext _context;
            public GetAllOrderQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<IEnumerable<Order>> Handle(GetAllOrderQuery request, CancellationToken cancellationToken)
            {
                var orderList = await _context.Orders.ToListAsync();
                if (orderList == null)
                {
                    return null;
                }
                return orderList.AsReadOnly();
            }

        }
    }
}
