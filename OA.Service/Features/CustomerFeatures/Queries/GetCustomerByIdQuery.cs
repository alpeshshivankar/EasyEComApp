using MediatR;
using ECom.Domain.Entities;
using ECom.Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ECom.Application.Features.CustomerFeatures.Queries
{
    public class GetCustomerByIdQuery : IRequest<Customer>
    {
        public int Id { get; set; }

        public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, Customer>
        {
            private readonly InMemoryDbContext _context;

            public GetCustomerByIdQueryHandler(InMemoryDbContext context)
            {
                _context = context;
            }

            public async Task<Customer> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
            {
                var customer = await _context.Customers.Where(a => a.Id == request.Id).FirstOrDefaultAsync();
                if (customer == null) return null;
                return customer;
            }
        }
    }
}