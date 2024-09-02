using ECom.Domain.Contract;
using ECom.Domain.Entities;
using ECom.Application.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ECom.Application.Features.CategoryFeatures.Queries
{
    public class GetAllCategoryQuery : IRequest<IEnumerable<Category>>
    {
        public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQuery, IEnumerable<Category>>
        {
            private readonly ICategoryRepository _context;

            public GetAllCategoryQueryHandler(ICategoryRepository context)
            {
                _context = context;
            }

            public async Task<IEnumerable<Category>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
            {
                var categoryList = await _context.GetAllCategories();
                return categoryList.Any() ? categoryList : new List<Category>(); ;
            }
        }
    }
}