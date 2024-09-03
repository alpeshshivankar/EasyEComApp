using ECom.Application.DTOs;
using ECom.Application.Features.CategoryFeatures.Commands;
using ECom.Application.Features.CategoryFeatures.Queries;
using ECom.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace ECom.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/Category")]
    [ApiVersion("1.0")]
    public class CategoryController : ControllerBase
    {
        private IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllCategoryQuery()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _mediator.Send(new GetCategoryByIdQuery { Id = id }));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CategoryDto categoryDto)
        {
            var result = await _mediator.Send(new CreateCategoryCommand(categoryDto));

            if (result.IsSuccess)
            {
                return CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value);
            }

            return BadRequest(result.Error);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryDto command)
        {
            var result = await _mediator.Send(new UpdateCategoryCommand(id, command));
            if (result.IsSuccess) return NoContent();
            if (result.Error == "Category not found.") return NotFound(result.Error);
            return BadRequest(result.Error);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(DeleteCategoryCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsSuccess) return NoContent(); 
            if (result.Error == "Category not found.") return NotFound(result.Error); 
            return BadRequest(result.Error); 
        }
    }
}