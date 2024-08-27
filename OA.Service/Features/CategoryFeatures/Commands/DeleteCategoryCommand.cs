using ECom.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ECom.Service.Features.CategoryFeatures.Commands
{
    public class DeleteCategoryCommand : IRequest<int>
    {
        public int Id { get; set; }
        public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, int>
        {
            private readonly IApplicationDbContext _context;
            public DeleteCategoryCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
            {
                var category = await _context.Categories.Where(a => a.Id == request.Id).FirstOrDefaultAsync();
                if (category == null) return default;
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
                return category.Id;
            }
        }
    }
}
