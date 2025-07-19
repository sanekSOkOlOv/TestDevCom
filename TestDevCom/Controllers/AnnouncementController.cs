using Microsoft.AspNetCore.Mvc;
using TestDevCom.Enums;
using TestDevCom.Models;
using TestDevCom.Services;

namespace BulletinBoard.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnnouncementController : ControllerBase
    {
        private readonly IAnnouncementService _service;

        public AnnouncementController(IAnnouncementService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? category, [FromQuery] string? subCategory)
        {
            var result = await _service.GetAllAsync(category, subCategory);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Announcement announcement)
        {
            if (!CategoryConfig.IsSubcategoryValid(announcement.Category, announcement.SubCategory))
                return BadRequest($"Підкатегорія '{announcement.SubCategory}' не відповідає категорії '{announcement.Category}'.");
            


            await _service.AddAsync(announcement);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Announcement announcement)
        {
            if (id != announcement.Id)
                return BadRequest("ID в URL не збігається з ID в тілі запиту");

            if (!CategoryConfig.IsSubcategoryValid(announcement.Category, announcement.SubCategory))
                return BadRequest($"Підкатегорія '{announcement.SubCategory}' не відповідає категорії '{announcement.Category}'.");
            

            await _service.UpdateAsync(announcement);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
