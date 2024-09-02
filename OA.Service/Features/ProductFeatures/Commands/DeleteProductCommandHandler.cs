using ECom.Application.Persistance;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ECom.Application.Features.ProductFeatures.Commands
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, int>
    {
        private readonly InMemoryDbContext _context;

        public DeleteProductCommandHandler(InMemoryDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = _context.Products.Where(a => a.Id == request.Id).FirstOrDefault();
            if (product == null) return default;
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return product.Id;
        }
    }
}