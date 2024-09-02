using MediatR;

namespace ECom.Service.Features.CustomerFeatures.Commands
{
    public class DeleteCustomerCommand : IRequest<int>
    {
        public int Id { get; set; }
        
    }
}
