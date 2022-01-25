using LightControl.UI.Models;
using LightControl.UI.Services;
using LightControl.UI.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LightControl.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHueLightService _lightService;

        public HomeController(ILogger<HomeController> logger, IHueLightService lightService)
        {
            _logger = logger;
            _lightService = lightService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> TestAPI()
        {
            var response = await _lightService.GetLights();

            ViewBag.Response = response;
            
            return View("Index");
        }
        
    }
}