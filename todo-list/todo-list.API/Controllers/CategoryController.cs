using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using todo_list.BusinessLogic.Interfaces;
using todo_list.BusinessLogic.Dtos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace todo_list.Server.Controllers
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> Get()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> Get(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
                return NotFound();

            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CategoryDto category)
        {
            var categoryEntity = await _categoryService.AddCategoryAsync(category);
            return CreatedAtAction(nameof(Get), new { id = categoryEntity.Id }, categoryEntity);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] CategoryDto category)
        {
            await _categoryService.UpdateCategoryAsync(category, id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return NoContent();
        }
    }
}
