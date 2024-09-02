using AutoMapper;
using ECom.Domain.Entities;
using ECom.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECom.Application.Features.OrderFeatures.Commands
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
    {
        private readonly InMemoryDbContext _context;
        private readonly IMapper _mapper;

        public CreateOrderCommandHandler(InMemoryDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<Order>(request);
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order != null ? order.Id : 0;
        }
    }
}