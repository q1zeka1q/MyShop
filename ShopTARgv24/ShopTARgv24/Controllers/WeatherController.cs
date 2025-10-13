using Microsoft.AspNetCore.Mvc;
using ShopTARgv24.Core.ServiceInterface;

namespace ShopTARgv24.Controllers
{
    public class WeatherController : Controller
    {

        private readonly IWeatherForecastServices  _weatherForecastsServices;

            public WeatherController
            (
                IWeatherForecastServices weatherForecastServices
            )
        {
            _weatherForecastsServices = weatherForecastServices;
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]

        public IActionResult SearchCity()
        {
            return View();
        }
    }
}
