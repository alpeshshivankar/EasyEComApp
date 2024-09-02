using MediatR;

namespace ECom.Application.Features.CategoryFeatures.Commands
{
    public class CreateCategoryCommand : IRequest<int>
    {
        public string CategoryName { get; set; }
        public string Description { get; set; }

    }
}
