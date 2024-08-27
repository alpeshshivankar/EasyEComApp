using MediatR;
using ECom.Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ECom.Domain.Entities;

namespace ECom.Service.Features.CustomerFeatures.Commands
{
    public class UpdateProductCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public class UpdateCustomerCommandHandler : IRequestHandler<UpdateProductCommand, int>
        {
            private readonly IApplicationDbContext _context;
            public UpdateCustomerCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
            {
                var cust = _context.Customers.Where(a => a.Id == request.Id).FirstOrDefault();

                if (cust == null)
                {
                    return default;
                }
                else
                {
                    cust.CustomerName = request.CustomerName;
                    cust.ContactName = request.ContactName;
                    cust.ContactTitle = request.ContactTitle;
                    cust.Address = request.Address;
                    cust.City = request.City;
                    cust.Region = request.Region;
                    cust.PostalCode = request.PostalCode;
                    cust.Country = request.Country;
                    cust.Phone = request.Phone;
                    cust.Fax = request.Fax;
                    _context.Customers.Update(cust);
                    await _context.SaveChangesAsync();
                    return cust.Id;
                }
            }
        }
    }
}
