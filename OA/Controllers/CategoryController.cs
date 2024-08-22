using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OA.Domain.Entities;
using OA.Service.Contract;

namespace OA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("{id}")]
        public IActionResult GetCategory(int id)
        {
            var category = _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpGet]
        public IActionResult GetAllCategories()
        {
            var categories = _categoryService.GetAllCategories();
            return Ok(categories);
        }

        [HttpPost]
        public IActionResult AddCategory([FromBody] Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdCategory = _categoryService.AddCategory(category);
            return CreatedAtAction(nameof(GetCategory), new { id = createdCategory.Id }, createdCategory);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id, [FromBody] Category category)
        {
            if (id != category.Id || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updatedCategory = _categoryService.UpdateCategory(category);
            if (updatedCategory == null)
            {
                return NotFound();
            }
            return Ok(updatedCategory);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var deleted = _categoryService.DeleteCategory(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
