using MediatR;
using Microsoft.EntityFrameworkCore;
using ECom.Domain.Entities;
using ECom.Persistence;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ECom.Application.Features.CustomerFeatures.Queries
{
    public class GetAllCustomerQuery : IRequest<IEnumerable<Customer>>
    {
        public class GetAllCustomerQueryHandler : IRequestHandler<GetAllCustomerQuery, IEnumerable<Customer>>
        {
            private readonly InMemoryDbContext _context;

            public GetAllCustomerQueryHandler(InMemoryDbContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<Customer>> Handle(GetAllCustomerQuery request, CancellationToken cancellationToken)
            {
                var customerList = await _context.Customers.ToListAsync();
                if (customerList == null)
                {
                    return null;
                }
                return customerList.AsReadOnly();
            }
        }
    }
}