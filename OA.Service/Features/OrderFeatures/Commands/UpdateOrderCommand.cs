using MediatR;
using ECom.Persistence;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using ECom.Domain.Entities;
using System;
using System.Collections.Generic;

namespace ECom.Application.Features.OrderFeatures.Commands
{
    public class UpdateOrderCommand : IRequest<int>
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? OrderFulfillmentDate { get; set; }
        public List<Product> ProductDetails { get; set; }
        public string OrderStatus { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}