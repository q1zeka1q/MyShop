﻿using Microsoft.AspNetCore.Mvc;

namespace ShopTARgv24.Controllers
{
    public class WeatherController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
