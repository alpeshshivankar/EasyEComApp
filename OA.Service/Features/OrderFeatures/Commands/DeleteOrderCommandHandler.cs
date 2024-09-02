using ECom.Application.Persistance;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ECom.Application.Features.OrderFeatures.Commands
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, int>
    {
        private readonly InMemoryDbContext _context;

        public DeleteOrderCommandHandler(InMemoryDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = _context.Orders.Where(a => a.Id == request.Id).FirstOrDefault();
            if (order == null) return default;
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return order.Id;
        }
    }
}