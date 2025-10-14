using Microsoft.AspNetCore.Mvc;
using ShopTARgv24.Core.Dto;
using ShopTARgv24.Core.ServiceInterface;
using ShopTARgv24.Models.Weather;

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

        public IActionResult SearchCity(AccuWeatherSearchViewModel model)
        {
            if (ModelState.IsValid)
            { 
                return RedirectToAction("City","Weather", new {city=model.CityName});
            }
            return View(model);
        }

        [HttpGet]

        public IActionResult City(string city)
        {
            AccuLocationWeatherResultDto dto = new();
            dto.CityName= city;

            _weatherForecastsServices.AccuWeatherResult(dto);

            return View();
        }
    }
}
