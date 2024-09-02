using MediatR;

namespace ECom.Application.Features.CategoryFeatures.Commands
{
    public class UpdateCategoryCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
    }
}