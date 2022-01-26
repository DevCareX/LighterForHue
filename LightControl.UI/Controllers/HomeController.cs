using HueCore.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

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