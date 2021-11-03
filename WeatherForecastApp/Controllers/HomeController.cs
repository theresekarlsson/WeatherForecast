using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;
using WeatherForecastApp.Models;
using WeatherForecastApp.Services;

namespace WeatherForecastApp.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IApiService _apiService;


        public HomeController(ILogger<HomeController> logger, IApiService apiService)
        {
            _logger = logger;
            _apiService = apiService;
        }
        public async Task<IActionResult> Index()
        {
            WeatherForecastViewModel weatherForecast = await _apiService.GetForecastAsync();
            return View(weatherForecast);
        }

        [HttpPost]
        public async Task<IActionResult> LoadForecast()
        {
            var weatherForecast = await _apiService.GetForecastAsync();
            return PartialView("_WeatherCurrentAndDaily", weatherForecast);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
