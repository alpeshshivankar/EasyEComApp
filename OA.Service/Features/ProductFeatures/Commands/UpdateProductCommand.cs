using MediatR;
using ECom.Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ECom.Service.Features.ProductFeatures.Commands
{
    public class UpdateProductCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int CategoryId { get; set; }
        public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, int>
        {
            private readonly IApplicationDbContext _context;
            public UpdateProductCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
            {
                var product = _context.Products.Where(a => a.Id == request.Id).FirstOrDefault();

                if (product == null)
                {
                    return default;
                }
                else
                {
                    product.ProductName = request.ProductName;
                    product.UnitPrice = request.UnitPrice;
                    product.CategoryId = request.CategoryId;
                    _context.Products.Update(product);
                    await _context.SaveChangesAsync();
                    return product.Id;
                }
            }
        }
    }
}
