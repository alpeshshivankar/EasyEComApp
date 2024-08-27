using MediatR;
using ECom.Domain.Entities;
using ECom.Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ECom.Service.Features.OrderFeatures.Commands
{
    public class CreateOrderCommand: IRequest<int>
    {
        public Customer Customers { get; set; }
        public int CustomerId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
        {
            private readonly IApplicationDbContext _context;
            public CreateOrderCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
            {
                var order = new Order();
                order.CustomerId = request.CustomerId;
                order.EmployeeId = request.EmployeeId;
                order.OrderDate = request.OrderDate;
                order.RequiredDate = request.RequiredDate;
                order.Customers = request.Customers;
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
                return order.Id;
            }
        }
    }
}
