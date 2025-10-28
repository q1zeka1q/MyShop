using Microsoft.AspNetCore.Mvc;
using ShopTARgv24.Core.ServiceInterface;

namespace ShopTARgv24.Controllers
{
    public class CocktailsController : Controller
    {
        private readonly ICocktailService _svc;
        public CocktailsController(ICocktailService svc) => _svc = svc;

        // /Cocktails/Search?q=margarita
        public async Task<IActionResult> Search(string? q)
        {
            q ??= "margarita"; // по заданию
            var data = await _svc.SearchByName(q);
            return View(data);
        }

        // /Cocktails/Details/11007
        public async Task<IActionResult> Details(string id)
        {
            var data = await _svc.LookupById(id);
            var drink = data?.drinks?.FirstOrDefault();
            if (drink == null) return NotFound();
            return View(drink);
        }
    }
}
