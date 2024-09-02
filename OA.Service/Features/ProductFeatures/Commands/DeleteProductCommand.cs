using MediatR;

namespace ECom.Application.Features.ProductFeatures.Commands
{
    public class DeleteProductCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}