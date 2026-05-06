using MVCProject.ITI.Models.Trip;

namespace MVCProject.ITI.Serviceslayer.Trip
{
    public class TripCostService : ITripCostService
    {
        private readonly IWeatherService _weather;
        private readonly IRouteService _routes;

        public TripCostService(IWeatherService weather, IRouteService routes)
        {
            _weather = weather;
            _routes = routes;
        }

        public async Task<TripCostCalculation> CalculateAsync(
            string origin,
            string destination,
            float distanceKm,
            float fuelPricePerLiter,
            float fuelEfficiencyL100km,
            int passengerCount,
            bool isAcOn)
        {
            // firstly we post the both API and work on them together
            // without waiting response of each other
            var weatherTask = _weather.GetWeatherAsync(destination);
            var routesTask = _routes.GetRoutesAsync(origin, destination);
            await Task.WhenAll(weatherTask, routesTask);

            var weather = await weatherTask;
            var routes = await routesTask;

            // we now choose the best route -> best route = lowest traffic and shortest duration
            var bestRoute = routes
                .OrderBy(r => r.TrafficCondition == "Low" ? 0 :
                              r.TrafficCondition == "Medium" ? 1 : 2)
                .ThenBy(r => r.DurationMinutes)
                .FirstOrDefault() ?? new RouteOption
                {
                    Summary = "Default Route",
                    DistanceKm = distanceKm,
                    DurationMinutes = (int)(distanceKm / 80 * 60),
                    TrafficCondition = "Medium"
                };


            // Calculate Weather Multiplier / AC -> Air Conditioning
            // if it found the wather is ( hot | windy | rainy )
            // and the AC is on → it will consume more fuel
           
            float weatherMult = 1.0f;
            if (weather.IsHot && isAcOn)
                weatherMult = 1.20f;   // +20% -> AC in hot weather
            else if (weather.IsRainy)
                weatherMult = 1.15f;   // +15% -> Rainy weather
            else if (weather.IsWindy)
                weatherMult = 1.10f;   // +10% -> Windy weather
            else if (weather.IsHot)
                weatherMult = 1.05f;   // +5% -> Hot weather

            // Calculate Traffic Multiplier
            // traffic cause the car to stop and go which increases fuel consumption,
            // especially in heavy traffic
       
            float trafficMult = bestRoute.TrafficCondition switch
            {
                "Low" => 1.00f,   // no traffic = no increase
                "Medium" => 1.10f,   // medium traffic = +10%
                "Heavy" => 1.25f,   // heavy traffic = +25%
                _ => 1.10f
            };

            // ───- Calaculation Of The Cost ─────   

            
            // feulLiters = (distanceKm * fuelEfficiencyL100km) / 100
            float fuelLiters = (bestRoute.DistanceKm * fuelEfficiencyL100km)
                               / 100f;

            // main cost of feul
            float baseCost = fuelLiters * fuelPricePerLiter;

            // final cost after applying all multipliers
            float totalCost = baseCost * weatherMult * trafficMult;


            // cost depend on the passangers
            int safePassengers = Math.Max(1, passengerCount);
            float costPerPassenger = totalCost / safePassengers;
            float costPerKm = totalCost / bestRoute.DistanceKm;

            return new TripCostCalculation
            {
                TotalCost = (float)Math.Round(totalCost, 2),
                CostPerKm = (float)Math.Round(costPerKm, 2),
                CostPerPassenger = (float)Math.Round(costPerPassenger, 2),
                FuelConsumed = (float)Math.Round(fuelLiters, 2),
                WeatherMultiplier = weatherMult,
                TrafficMultiplier = trafficMult,
                WeatherCondition = weather.Condition,
                TrafficCondition = bestRoute.TrafficCondition,
                AvailableRoutes = routes,
                SelectedRoute = bestRoute
            };
        }
    }
}