using Microsoft.EntityFrameworkCore;
using MVCProject.ITI.DataAccessLayer.Data;
using MVCProject.ITI.DataAccessLayer.Entities;
using MVCProject.ITI.Models.Trip;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MVCProject.ITI.Serviceslayer.Trip
{
    public class TripCostService : ITripCostService
    {
        private readonly IWeatherService _weather;
        private readonly IRouteService _routes;
        private readonly ApplicationDbContext _context;
        private const decimal FuelPricePerLiter = 18.5m;
        private const decimal MaintenanceCostPerKm = 0.75m;

        public TripCostService(IWeatherService weather, IRouteService routes, ApplicationDbContext context)
        {
            _weather = weather;
            _routes = routes;
            _context = context;
        }

        public async Task<TripCostResult> CalculateTripCostAsync(Guid vehicleId, string origin, string destination, bool isAcOn, DateTime tripDateTime)
        {
            var weather = await _weather.GetWeatherAsync(destination);
            var routes = await _routes.GetRoutesAsync(origin, destination);
            var bestRoute = routes.FirstOrDefault();

            if (bestRoute == null) throw new Exception("No route found");

            float consumptionRate = 8.0f;
            if (isAcOn) consumptionRate += 1.5f;

            float totalFuelLiters = (bestRoute.DistanceKm / 100.0f) * consumptionRate;
            decimal fuelCost = (decimal)totalFuelLiters * FuelPricePerLiter;
            decimal maintenanceCost = (decimal)bestRoute.DistanceKm * MaintenanceCostPerKm;

            var trafficInfo = GetTrafficFactor(tripDateTime);
            var weatherMult = GetWeatherMultiplier(weather);
            decimal totalCost = (fuelCost + maintenanceCost) * (decimal)(trafficInfo.multiplier * weatherMult);

            return new TripCostResult
            {
                Id = Guid.NewGuid(),
                FuelConsumed = (float)Math.Round(totalFuelLiters, 2),
                TotalCost = (float)Math.Round(totalCost, 2),
                CostPerKm = (float)Math.Round(totalCost / (decimal)bestRoute.DistanceKm, 2),
                CalculatedAt = DateTime.Now,
                WeatherCondition = weather?.Condition ?? "Normal",
                TrafficCondition = trafficInfo.label,
                WeatherMultiplier = (float)weatherMult,
                TrafficMultiplier = (float)trafficInfo.multiplier,
                FuelPriceId = _context.FuelPrices.OrderByDescending(p => p.RecordedDate).FirstOrDefault()?.Id ?? Guid.Empty
            };
        }

        private (string label, double multiplier) GetTrafficFactor(DateTime dt)
        {
            var hour = dt.Hour;
            if ((hour >= 7 && hour < 10) || (hour >= 16 && hour < 19)) return ("Heavy", 1.25);
            if (hour >= 23 || hour < 6) return ("Low", 0.95);
            return ("Medium", 1.0);
        }

        private double GetWeatherMultiplier(WeatherResult weather)
        {
            if (weather == null) return 1.0;
            var cond = weather.Condition.ToLower();
            if (cond.Contains("rain") || cond.Contains("storm")) return 1.15;
            return 1.0;
        }
    }
}