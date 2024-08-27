using ECom.Persistence;
using ECom.Service.Features.CategoryFeatures.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ECom.Service.Features.CategoryFeatures.Commands
{
    public class UpdateCategoryCommand: IRequest<int>
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, int>
        {
            private readonly IApplicationDbContext _context;
            public UpdateCategoryCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
            {
                var category = _context.Categories.Where(a => a.Id == request.Id).FirstOrDefault();

                if (category == null)
                {
                    return default;
                }
                else
                {
                    category.CategoryName = request.CategoryName;
                    category.Description = request.Description;
                    _context.Categories.Update(category);
                    await _context.SaveChangesAsync();
                    return category.Id;
                }
            }
        }
    }
}
