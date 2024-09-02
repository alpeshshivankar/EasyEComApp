using AutoMapper;
using ECom.Application.Persistance;
using ECom.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECom.Service.Features.CustomerFeatures.Commands
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, int>
    {
        private readonly InMemoryDbContext _context;
        private readonly IMapper _mapper;

        public CreateCustomerCommandHandler(InMemoryDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = _mapper.Map<Customer>(request);
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer.Id;
        }
    }
}