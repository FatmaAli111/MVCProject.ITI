using System.Text.Json;
using MVCProject.ITI.Models.Trip;

namespace MVCProject.ITI.Serviceslayer.Trip
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _http;
        private readonly string _apiKey; // in the appsettings.json

        // Constructor => work when new WeatherService() create
        // IConfiguration from Dependency Injection and read the appsettings.json
        public WeatherService(HttpClient http, IConfiguration config)
        {
            _http = http;
            _apiKey = config["ExternalApis:OpenWeatherApiKey"]
                      ?? throw new Exception("OpenWeatherApiKey is missing!");
        }

        public async Task<WeatherResult> GetWeatherAsync(string cityName)
        {
            var url = $"https://api.weatherapi.com/v1/current.json" +
                      $"?key={_apiKey}&q={cityName}";

            var response = await _http.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            var current = root.GetProperty("current");

            var temp = current
                .GetProperty("temp_c")
                .GetSingle();

            var condition = current
                .GetProperty("condition")
                .GetProperty("text")
                .GetString() ?? "Clear";

            var windKph = current
                .GetProperty("wind_kph")
                .GetSingle();

            return new WeatherResult
            {
                CityName = cityName,
                TemperatureC = temp,
                Condition = condition,
                WindSpeedMps = windKph / 3.6f
            };
        }

    }
}
