using AutoMapper;
using ECom.Persistence;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ECom.Application.Features.ProductFeatures.Commands
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, int>
    {
        private readonly InMemoryDbContext _context;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(InMemoryDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
                _mapper.Map(request, product);
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
                return product.Id;
            }
        }
    }
}