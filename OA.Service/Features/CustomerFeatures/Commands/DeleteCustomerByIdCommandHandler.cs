using ECom.Application.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ECom.Service.Features.CustomerFeatures.Commands
{
    public class DeleteCustomerByIdCommandHandler : IRequestHandler<DeleteCustomerCommand, int>
    {
        private readonly InMemoryDbContext _context;

        public DeleteCustomerByIdCommandHandler(InMemoryDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _context.Customers.Where(a => a.Id == request.Id).FirstOrDefaultAsync();
            if (customer == null) return default;
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return customer.Id;
        }
    }
}