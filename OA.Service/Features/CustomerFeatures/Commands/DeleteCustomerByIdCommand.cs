using MediatR;
using Microsoft.EntityFrameworkCore;
using ECom.Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ECom.Service.Features.CustomerFeatures.Commands
{
    public class DeleteProductCommand : IRequest<int>
    {
        public int Id { get; set; }
        public class DeleteCustomerByIdCommandHandler : IRequestHandler<DeleteProductCommand, int>
        {
            private readonly IApplicationDbContext _context;
            public DeleteCustomerByIdCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
            {
                var customer = await _context.Customers.Where(a => a.Id == request.Id).FirstOrDefaultAsync();
                if (customer == null) return default;
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
                return customer.Id;
            }
        }
    }
}
