using ECom.Application.DTOs;
using MediatR;

namespace ECom.Application.Features.CategoryFeatures.Commands
{
    public class DeleteCategoryCommand : IRequest<Result<int>>
    {
        public int Id { get; }

        public DeleteCategoryCommand(int id)
        {
            Id = id;
        }
    }
}