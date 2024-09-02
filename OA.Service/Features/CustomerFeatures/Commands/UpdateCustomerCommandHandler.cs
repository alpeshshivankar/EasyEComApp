using AutoMapper;
using ECom.Persistence;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ECom.Service.Features.CustomerFeatures.Commands
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, int>
    {
        private readonly InMemoryDbContext _context;
        private readonly IMapper _mapper;

        public UpdateCustomerCommandHandler(InMemoryDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var cust = _context.Customers.Where(a => a.Id == request.Id).FirstOrDefault();

            if (cust == null)
            {
                return default;
            }
            else
            {
                _mapper.Map(cust, request);
                _context.Customers.Update(cust);
                await _context.SaveChangesAsync();
                return cust.Id;
            }
        }
    }
}
