using AutoMapper;
using ECom.Application.Persistance;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ECom.Application.Features.OrderFeatures.Commands
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, int>
    {
        private readonly InMemoryDbContext _context;
        private readonly IMapper _mapper;

        public UpdateOrderCommandHandler(InMemoryDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = _context.Orders.Where(a => a.Id == request.Id).FirstOrDefault();
            if (order == null) return default;
            _mapper.Map(request, order);
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return order.Id;
        }
    }
}