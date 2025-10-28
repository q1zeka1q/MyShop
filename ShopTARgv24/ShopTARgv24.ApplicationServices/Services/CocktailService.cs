using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ShopTARgv24.Core.Dto;
using ShopTARgv24.Core.ServiceInterface;

namespace ShopTARgv24.ApplicationServices.Services
{
    public class CocktailService : ICocktailService
    {
        private readonly HttpClient _http;

        public CocktailService(HttpClient http) => _http = http;

        public async Task<DrinkResponseDto?> SearchByName(string name)
        {
            var url = $"https://www.thecocktaildb.com/api/json/v1/1/search.php?s={name}";
            var resp = await _http.GetAsync(url);
            if (!resp.IsSuccessStatusCode) return null;
            var json = await resp.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<DrinkResponseDto>(json);
        }

        public async Task<DrinkResponseDto?> LookupById(string id)
        {
            var url = $"https://www.thecocktaildb.com/api/json/v1/1/lookup.php?i={id}";
            var resp = await _http.GetAsync(url);
            if (!resp.IsSuccessStatusCode) return null;
            var json = await resp.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<DrinkResponseDto>(json);
        }
    }
}
