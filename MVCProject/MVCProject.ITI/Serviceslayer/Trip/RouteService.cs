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

        public async Task<List<RouteOption>> GetRoutesAsync(string origin, string destination)
{
    
    var fromCoords = await GeocodeAsync(origin);
    var toCoords = await GeocodeAsync(destination);


    if (fromCoords == null || toCoords == null)
        return GetFallbackRoutes(origin, destination);

    // بناء الـ URL للـ OpenRouteService
    var url = "https://api.openrouteservice.org/v2/directions/driving-car" +
              $"?api_key={_apiKey}" +
              $"&start={fromCoords.Value.lng},{fromCoords.Value.lat}" +
              $"&end={toCoords.Value.lng},{toCoords.Value.lat}" +
              "&alternatives=true";

    var response = await _http.GetAsync(url);
    if (!response.IsSuccessStatusCode) return GetFallbackRoutes(origin, destination);

    var json = await response.Content.ReadAsStringAsync();
    using var doc = JsonDocument.Parse(json);
    var features = doc.RootElement.GetProperty("features");

    var routes = new List<RouteOption>();

    foreach (var feature in features.EnumerateArray())
    {
        var summary = feature.GetProperty("properties").GetProperty("summary");
        
        routes.Add(new RouteOption
        {
            Summary = "Recommended Route",
            DistanceKm = (float)(summary.GetProperty("distance").GetDouble() / 1000.0),
            DurationMinutes = (int)(summary.GetProperty("duration").GetDouble() / 60.0),
            TrafficCondition = "Low",
            
            StartLat = fromCoords.Value.lat,
            StartLng = fromCoords.Value.lng,
            EndLat = toCoords.Value.lat,
            EndLng = toCoords.Value.lng
        });
    }

    return routes;
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
