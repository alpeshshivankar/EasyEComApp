using AutoMapper;
using ECom.Domain.Entities;
using ECom.Application.Persistance;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECom.Application.Features.ProductFeatures.Commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly InMemoryDbContext _context;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(InMemoryDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(request);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product != null ? product.Id : 0;
        }
    }
}