using MediatR;
using ECom.Persistence;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using ECom.Domain.Entities;
using System;

namespace ECom.Service.Features.OrderFeatures.Commands
{
    public class UpdateOrderCommand:IRequest<int>
    {
        public int Id { get; set; }
        public Customer Customers { get; set; }
        public int CustomerId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, int>
        {
            private readonly IApplicationDbContext _context;
            public UpdateOrderCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
            {
                var order = _context.Orders.Where(a => a.Id == request.Id).FirstOrDefault();

                if (order == null)
                {
                    return default;
                }
                else
                {
                    order.CustomerId = request.CustomerId;
                    order.EmployeeId = request.EmployeeId;
                    order.OrderDate = request.OrderDate;
                    order.RequiredDate = request.RequiredDate;
                    _context.Orders.Update(order);
                    await _context.SaveChangesAsync();
                    return order.Id;
                }
            }
        }
    }
}
