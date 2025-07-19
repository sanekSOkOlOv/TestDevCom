using Microsoft.AspNetCore.Mvc;
using TestDevCom.Enums;

namespace TestDevCom.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        [HttpGet("subcategories/byid/{categoryId:int}")]
        public IActionResult GetSubcategoriesById(int categoryId)
        {
            if (!Enum.IsDefined(typeof(Category), categoryId))
                return NotFound();

            var parsedCategory = (Category)categoryId;

            if (!CategoryConfig.Map.TryGetValue(parsedCategory, out var subcategories))
                return NotFound();

            return Ok(subcategories);
        }

    }
}
