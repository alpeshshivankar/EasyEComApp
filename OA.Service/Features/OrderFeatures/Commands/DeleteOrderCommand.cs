using MediatR;

namespace ECom.Application.Features.OrderFeatures.Commands
{
    public class DeleteOrderCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}