using MediatR;

namespace ECom.Application.Features.CategoryFeatures.Commands
{
    public class DeleteCategoryCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}