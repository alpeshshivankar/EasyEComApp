using MediatR;
using ECom.Domain.Entities;
using ECom.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace ECom.Service.Features.ProductFeatures.Commands
{
    public class CreateProductCommand : IRequest<int>
    {
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int CategoryId { get; set; }
        public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
        {
            private readonly IApplicationDbContext _context;
            public CreateProductCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
            {
                var product = new Product();
                product.ProductName = request.ProductName;
                product.UnitPrice = request.UnitPrice;
                product.CategoryId = request.CategoryId;

                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return product.Id;
            }
        }
    }
}
