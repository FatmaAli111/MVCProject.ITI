using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCProject.ITI.DataAccessLayer.Data;
using MVCProject.ITI.DataAccessLayer.Entities;
using MVCProject.ITI.Serviceslayer.Trip;
using MVCProject.ITI.ViewModels;
using System.Security.Claims;

namespace MVCProject.ITI.Controllers
{
    public class NewTripRequest
    {
        public string From { get; set; } = string.Empty;
        public string To { get; set; } = string.Empty;
        public Guid VehicleId { get; set; }
        public bool IsAcOn { get; set; }
        public bool LeaveNow { get; set; }
        public DateTime? ScheduledTime { get; set; }
    }

    [Authorize]
    public class TripController : Controller
    {
        private readonly IRouteService _routeService;
        private readonly ITripCostService _costService;
        private readonly ApplicationDbContext _context;

        public TripController(IRouteService routeService, ITripCostService costService, ApplicationDbContext context)
        {
            _routeService = routeService;
            _costService = costService;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> StartNewTrip()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim)) return RedirectToAction("Login", "Account");
            var userId = Guid.Parse(userIdClaim);
            var vehicles = await _context.Vehicles.Where(v => v.UserId == userId).ToListAsync();
            var viewModel = new NewTripViewModel
            {
                AvailableVehicles = vehicles.Select(v => new SelectListItem { Value = v.Id.ToString(), Text = v.NickName }).ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> StartTrip([FromBody] NewTripRequest request)
        {
            try
            {
                var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var userId = Guid.Parse(userIdClaim);
                var costResult = await _costService.CalculateTripCostAsync(request.VehicleId, request.From, request.To, request.IsAcOn, request.LeaveNow ? DateTime.Now : request.ScheduledTime ?? DateTime.Now);

                var routes = await _routeService.GetRoutesAsync(request.From, request.To);
                var bestRoute = routes.First();

                var trip = new Trip
                {
                    UserId = userId,
                    VehicleId = request.VehicleId,
                    OriginName = request.From,
                    DestinationName = request.To,
                    DistanceKm = bestRoute.DistanceKm,
                    DurationMinutes = bestRoute.DurationMinutes,
                    IsAcOn = request.IsAcOn,
                    PassengerCount = 1,
                    TripDate = DateTime.Now,
                    CreatedAt = DateTime.Now
                };

                _context.Trips.Add(trip);
                await _context.SaveChangesAsync();

                costResult.TripId = trip.Id;
                _context.TripCostResults.Add(costResult);
                await _context.SaveChangesAsync();

                return Json(new { success = true, redirectUrl = Url.Action("CompletionTrip", new { id = trip.Id }) });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public async Task<IActionResult> CompletionTrip(Guid id)
        {
            var trip = await _context.Trips
                .Include(t => t.Vehicle)
                .Include(t => t.TripCostResult)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (trip == null) return NotFound();

            var routes = await _routeService.GetRoutesAsync(trip.OriginName, trip.DestinationName);
            var bestRoute = routes.FirstOrDefault();

            return View(new CompletionTripViewModel
            {
                TripId = trip.Id,
                FromName = trip.OriginName,
                ToName = trip.DestinationName,
                DistanceKm = trip.DistanceKm,
                DurationMinutes = trip.DurationMinutes,
                TripDate = trip.TripDate,
                FromLat = bestRoute?.StartLat ?? 0,
                FromLng = bestRoute?.StartLng ?? 0,
                ToLat = bestRoute?.EndLat ?? 0,
                ToLng = bestRoute?.EndLng ?? 0,
                CarName = trip.Vehicle?.NickName ?? "Vehicle",
                TotalCost = trip.TripCostResult?.TotalCost ?? 0,
                FuelConsumed = trip.TripCostResult?.FuelConsumed ?? 0,
                TrafficCondition = trip.TripCostResult?.TrafficCondition ?? "Normal",
                WeatherCondition = trip.TripCostResult?.WeatherCondition ?? "Clear",
                FuelCost = (trip.TripCostResult?.FuelConsumed ?? 0) * 18.5,
                MaintenanceCost = trip.DistanceKm * 0.75
            });
        }

        [HttpPost]
        public async Task<IActionResult> ToggleFavorite(Guid tripId)
        {
            var trip = await _context.Trips.FindAsync(tripId);
            if (trip == null)
                return Json(new { success = false, message = "Trip not found" });

            trip.IsFavorite = !trip.IsFavorite;
            await _context.SaveChangesAsync();
            return Json(new { success = true, isFavorite = trip.IsFavorite });
        }

        [HttpPost]
        public async Task<IActionResult> SavePassengers([FromBody] SavePassengersRequest request)
        {
            if (request.TripId == Guid.Empty)
                return Json(new { success = false, message = "Invalid Trip Id" });

            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTrip(Guid id)
        {
            var trip = await _context.Trips.FindAsync(id);
            if (trip == null) return Json(new { success = false });
            _context.Trips.Remove(trip);
            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }

        public async Task<IActionResult> History()
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var history = await _context.Trips
                .Include(t => t.Vehicle)
                .Include(t => t.TripCostResult)
                .Where(t => t.UserId == userId)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
            return View(history);
        }
    }
}