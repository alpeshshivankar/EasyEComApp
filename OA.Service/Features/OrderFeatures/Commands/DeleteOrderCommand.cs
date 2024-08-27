using MediatR;
using ECom.Persistence;
using ECom.Service.Features.CustomerFeatures.Commands;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace ECom.Service.Features.OrderFeatures.Commands
{
    public class DeleteOrderCommand:IRequest<int>
    {
        public int Id { get; set; }
        public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, int>
        {
            private readonly IApplicationDbContext _context;
            public DeleteOrderCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
            {
                var order = await _context.Customers.Where(a => a.Id == request.Id).FirstOrDefaultAsync();
                if (order == null) return default;
                _context.Customers.Remove(order);
                await _context.SaveChangesAsync();
                return order.Id;
            }
        }
    }
}
