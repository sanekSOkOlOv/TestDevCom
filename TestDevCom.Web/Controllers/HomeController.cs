using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TestDevCom.Web.Enums;
using TestDevCom.Web.Models;
using TestDevCom.Web.Services;

namespace TestDevCom.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAnnouncementService _service;

        public HomeController(IAnnouncementService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index(string? category, string? subCategory)
        {
            string? categoryName = null;

            if (int.TryParse(category, out int catValue) &&
                Enum.IsDefined(typeof(Category), catValue))
            {
                categoryName = Enum.GetName(typeof(Category), catValue);
            }

            var items = await _service.GetAllAsync(categoryName, subCategory);

            ViewBag.SelectedCategory = category;
            ViewBag.SelectedSubCategory = subCategory;
            return View(items);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(Announcement model)
        {
            if (await _service.CreateAsync(model))
                return RedirectToAction("Index");

            return View(model);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null)
            {
                Console.WriteLine($"Не знайдено оголошення з ID: {id}");
                return NotFound();
            }

            return View(item);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(Announcement model)
        {
            if (await _service.UpdateAsync(model))
                return RedirectToAction("Index");

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }

}
