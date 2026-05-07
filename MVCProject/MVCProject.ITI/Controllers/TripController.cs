using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCProject.ITI.DataAccessLayer.Data;
using MVCProject.ITI.DataAccessLayer.Entities;
using MVCProject.ITI.Models;
using MVCProject.ITI.Serviceslayer;
using MVCProject.ITI.Serviceslayer.Trip;

namespace MVCProject.ITI.Controllers
{
    // ── Request Models ────────────────────────────────────────────────────────
    // ( SavePassengersRequest - PassengerItem ) for saving data in javascript to DB
    public class SavePassengersRequest
    {
        public Guid TripId { get; set; }
        public List<PassengerItem> Passengers { get; set; } = new();
    }

    public class PassengerItem
    {
        public string Name { get; set; } = string.Empty;
        public float ShareAmount { get; set; }
        public float SharePercentage { get; set; }
    }

    // ── Controller ────────────────────────────────────────────────────────────
    [Authorize]

    public class TripController : Controller
    {
        private readonly IWeatherService _weatherService;
        private readonly IRouteService _routeService;
        private readonly ITripCostService _costService;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRecentTripService _recentTripService;

        public TripController(
            IWeatherService weatherService,
            IRouteService routeService,
            ITripCostService costService,
            ApplicationDbContext context
            , UserManager<ApplicationUser> userManager
            , VehicleService vechileService
            , IRecentTripService recentTripService)
        {
            _weatherService = weatherService;
            _routeService = routeService;
            _costService = costService;
            _context = context;
            _userManager = userManager;
            _recentTripService = recentTripService;
        }

        // GET /Trip/History
        public async Task<IActionResult> History()
        {
            try
            {
                ApplicationUser user = await _userManager.GetUserAsync(User);
                if (user is null)
                    return Redirect("/Identity/Account/Login");

                Guid id = user.Id;
                
                IEnumerable<TripCardViewModel> Alltrips = await _recentTripService.GetAllTrips(id);
                return View(Alltrips);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", ex.Message);
            }
        }

        // GET /Trip/CompletionTrip
        public IActionResult CompletionTrip()
        {
            // test for waiting insertion in DB
            return View();
        }

        // POST /Trip/SavePassengers
        // Take Passengers from Javasscript and Save them in DB
        [HttpPost]
        public async Task<IActionResult> SavePassengers(
            [FromBody] SavePassengersRequest request)
        {
            // Not Saving Trip Id if it's not assigned
            if (request.TripId == Guid.Empty)
                return Json(new { success = false, message = "Invalid Trip Id" });

            // Delete Old Passengers In The Trip if it found
            var existing = _context.TripPassengers
                .Where(p => p.TripId == request.TripId);
            _context.TripPassengers.RemoveRange(existing);

            // Add New Passengers
            foreach (var p in request.Passengers)
            {
                _context.TripPassengers.Add(new TripPassenger
                {
                    Id = Guid.NewGuid(),
                    TripId = request.TripId,
                    Name = p.Name,
                    ShareAmount = p.ShareAmount,
                    SharePercentage = p.SharePercentage
                });
            }

            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }

        // ── Test Actions  ───────────

        public async Task<IActionResult> TestWeather()
        {
            var result = await _weatherService.GetWeatherAsync("Cairo");
            return Content($"Temp: {result.TemperatureC}°C | " +
                           $"Condition: {result.Condition} | " +
                           $"IsHot: {result.IsHot} | " +
                           $"IsRainy: {result.IsRainy}");
        }

        public async Task<IActionResult> TestRoute()
        {
            var routes = await _routeService.GetRoutesAsync("Cairo", "Alexandria");
            var result = string.Join(" | ", routes.Select(r =>
                $"{r.Summary}: {r.DistanceKm:F0}km, {r.DurationMinutes}min, {r.TrafficCondition}"));
            return Content(result);
        }

        public async Task<IActionResult> TestCost()
        {
            var result = await _costService.CalculateAsync(
                origin: "Cairo",
                destination: "Alexandria",
                distanceKm: 220,
                fuelPricePerLiter: 13.75f,
                fuelEfficiencyL100km: 8.5f,
                passengerCount: 1,
                isAcOn: true);

            return Content(
                $"Total: EGP {result.TotalCost} | " +
                $"Per KM: {result.CostPerKm} | " +
                $"Weather: {result.WeatherCondition} ({result.WeatherMultiplier}x) | " +
                $"Traffic: {result.TrafficCondition} ({result.TrafficMultiplier}x) | " +
                $"Fuel: {result.FuelConsumed}L");
        }
    }
}