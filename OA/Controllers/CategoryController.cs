using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ECom.Domain.Entities;
using ECom.Service.Features.CustomerFeatures.Queries;
using MediatR;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ECom.Service.Features.CustomerFeatures.Commands;
using ECom.Service.Features.CategoryFeatures.Commands;

namespace ECom.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/Category")]
    [ApiVersion("1.0")]
    public class CategoryController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllCategoryQuery()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetCategoryByIdQuery { Id = id }));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateCategoryCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteCategoryCommand { Id = id }));
        }
    }
}
