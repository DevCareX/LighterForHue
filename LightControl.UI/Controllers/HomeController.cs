using LightControl.UI.Models;
using LightControl.UI.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LightControl.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> TestAPI()
        {
            HueHttpClient hueHttpClient = new HueHttpClient(_configuration.GetSection("HueSettings"));
            var response = await hueHttpClient.APITest();

            ViewBag.Response = response;
            
            return View("Index");
        }
        
    }
}