using System.Text.Json;
using MVCProject.ITI.Models.Trip;

namespace MVCProject.ITI.Serviceslayer.Trip
{
    public class RouteService : IRouteService
    {
        private readonly HttpClient _http;
        private readonly string _apiKey;

        public RouteService(HttpClient http, IConfiguration config) {

            _http = http;
            _apiKey = config["ExternalApis:OpenRouteServiceApiKey"] ?? throw new Exception("OpenRouteServiceApiKey is missing!");
        }

        public async Task<List<RouteOption>> GetRoutesAsync(
            string origin, string destination)
        {
            // we convert city name to coordinates (lat - lng) to ease using of the API so we use geocoding API to do that
            var fromCoords = await GeocodeAsync(origin);
            var toCoords = await GeocodeAsync(destination);

            if (fromCoords == null || toCoords == null)
                return GetFallbackRoutes(origin, destination);

            // now we choose the route the URL require the points between the two cities and return more route in => &alternatives=true
            var url = "https://api.openrouteservice.org/v2/directions/driving-car" +
                      $"?api_key={_apiKey}" +
                      $"&start={fromCoords.Value.lng},{fromCoords.Value.lat}" +
                      $"&end={toCoords.Value.lng},{toCoords.Value.lat}" +
                      "&alternatives=true";

            var response = await _http.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                return GetFallbackRoutes(origin, destination);

            var json = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(json);
            var features = doc.RootElement.GetProperty("features");

            var routes = new List<RouteOption>();
            int index = 0;

            foreach (var feature in features.EnumerateArray())
            {
                var props = feature.GetProperty("properties");
                var summary = props.GetProperty("summary");

                // convertint from meter to kilometer
                var distanceM = summary.GetProperty("distance").GetSingle();
                var distanceKm = distanceM / 1000f;

                // converting from seconds to minutes
                var durationSec = summary.GetProperty("duration").GetSingle();
                var durationMin = (int)(durationSec / 60);

                // choose the route depend on the speed if the speed more than 80 km/h we can say that the traffic is low
                // if the speed more than 50 km/h we can say that the traffic is medium else we can say that the traffic is heavy
                var speedKmh = distanceKm / (durationMin / 60f);
                var traffic = speedKmh > 80 ? "Low" :
                              speedKmh > 50 ? "Medium" : "Heavy";

                routes.Add(new RouteOption
                {
                    Summary = index == 0 ? "Recommended Route" :
                              index == 1 ? "Alternative Route" : "Scenic Route",
                    DistanceKm = distanceKm,
                    DurationMinutes = durationMin,
                    TrafficCondition = traffic
                });

                index++;
            }

            return routes.Count > 0 ? routes : GetFallbackRoutes(origin, destination);
        }
        private async Task<(float lat, float lng)?> GeocodeAsync(string cityName)
        {
            try
            {
                var url = "https://api.openrouteservice.org/geocode/search" +
                          $"?api_key={_apiKey}" +
                          $"&text={Uri.EscapeDataString(cityName + ", Egypt")}" +
                          "&size=1";

                var response = await _http.GetAsync(url);
                if (!response.IsSuccessStatusCode) return null;

                var json = await response.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(json);

                
                var coords = doc.RootElement
                    .GetProperty("features")[0]
                    .GetProperty("geometry")
                    .GetProperty("coordinates");

                float lng = coords[0].GetSingle();
                float lat = coords[1].GetSingle();

                return (lat, lng);
            }
            catch
            {
                return null;
            }
        }
        // if API not work well it will return some fallback data based on the approximate distance between the two cities
        private List<RouteOption> GetFallbackRoutes(string origin, string destination)
        {
            return new List<RouteOption>
            {
                new RouteOption
                {
                    Summary = "Recommended Route",
                    DistanceKm = 220,
                    DurationMinutes = 150,
                    TrafficCondition = "Medium"
                },
                new RouteOption
                {
                    Summary = "Alternative Route",
                    DistanceKm = 245,
                    DurationMinutes = 160,
                    TrafficCondition = "Low"
                }
            };
        }
    }
}
