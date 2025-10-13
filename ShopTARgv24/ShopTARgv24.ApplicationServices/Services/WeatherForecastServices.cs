using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ShopTARgv24.Core.Dto;
using ShopTARgv24.Core.ServiceInterface;

namespace ShopTARgv24.ApplicationServices.Services
{
    public class WeatherForecastServices : IWeatherForecastServices
    {
        public async Task<AccuLocationWeatherResultDto> AccuWeatherResult(AccuLocationWeatherResultDto dto)
        { 
            string accuApiKey = "your_api";
            string baseUrl = "http://dataservice.accuweather.com/locations/v1/cities/search";

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var responce = await httpClient.GetAsync($"{127964}?apikay={accuApiKey}&details=true");
                    if (responce.IsSuccessStatusCode)
                {
                    var jsonResponce = await responce.Content.ReadAsStringAsync();
                    var weatherData = JsonSerializer.Deserialize< AccuLocationWeatherResultDto>(jsonResponce);
                    return weatherData;
                }
                    else
                {
                    throw new Exception("Error retrieving weather data");
                }
            }
        }
    }
}
