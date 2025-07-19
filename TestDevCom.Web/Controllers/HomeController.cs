using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using TestDevCom.Web.Models; 

namespace TestDevCom.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public HomeController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _clientFactory.CreateClient("TestDevComAPI");
            var response = await client.GetAsync("/api/Announcement");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var announcements = JsonConvert.DeserializeObject<List<Announcement>>(json);
                return View(announcements);
            }

            return View(new List<Announcement>());
        }
    }
}
