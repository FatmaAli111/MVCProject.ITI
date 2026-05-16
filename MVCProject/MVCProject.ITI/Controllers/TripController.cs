using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCProject.ITI.DataAccessLayer.Data;
using MVCProject.ITI.DataAccessLayer.Entities;
using MVCProject.ITI.Models;
using MVCProject.ITI.Serviceslayer;
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
        private readonly ITripService _tripService;
        private readonly IRecentTripService _recentTripService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly VehicleService _vehicleService;

        public TripController(IRouteService routeService, ITripCostService costService,
            ApplicationDbContext context, ITripService tripService,
            IRecentTripService recentTripService,UserManager<ApplicationUser> userManager,VehicleService vehicleService)
        {
            _routeService = routeService;
            _costService = costService;
            _context = context;
            _tripService = tripService;
           _recentTripService = recentTripService;
            _userManager = userManager;
            _vehicleService = vehicleService;
        }

        [HttpGet]
        public async Task<IActionResult> StartNewTrip()
        {
            try
            {
                ApplicationUser user = await _userManager.GetUserAsync(User);
                if (user is null)
                    return RedirectToPage("/Account/Login", new { area = "Identity" });

                Guid id = user.Id;
                ViewBag.Vehicle = await _vehicleService.GetDefaultVehicleAsync(id);

                IEnumerable<TripCardViewModel> AllTrips = await _recentTripService.GetAllTrips(id);
                ViewData["AllTrips"] = AllTrips;
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> StartTrip(NewTripViewModel request)
        {
            try
            {
                var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var userId = Guid.Parse(userIdClaim);
                var costResult = await _costService.CalculateTripCostAsync(request.VehicleId, request.From, request.To, request.IsAcOn, request.LeaveNow ? DateTime.Now : request.ScheduledTime ?? DateTime.Now);

                var routes = await _routeService.GetRoutesAsync(request.From, request.To);
                var bestRoute = routes.First();
                var vehicle = await _vehicleService.GetDefaultVehicleAsync(userId);
                request.VehicleId = vehicle.Id;
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

                return RedirectToAction("CompletionTrip", new { id = trip.Id });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
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
        public async Task<IActionResult> DeleteTrip(Guid id)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var trip = await _context.Trips.FindAsync(id);
            if (trip == null || trip.UserId != userId)
                return Json(new { success = false, message = "Trip not found" });
            _context.Trips.Remove(trip);
            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }

        public async Task<IActionResult> History(int page = 1)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var history = await _recentTripService.GetTripsPagedAsync(userId, page, 10);
            return View(history);
        }

        public async Task<IActionResult> CompletionTrip(Guid id)
        {

            var vm = await _tripService.GetCompletionTripAsync(id);

            if (vm == null)
                return NotFound();

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> SaveSplit([FromBody] SplitTripViewModel vm)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var result = await _tripService.SaveTripSplitAsync(vm, userId);

            if (!result)
                return BadRequest(new { success = false });

            return Ok(new { success = true });
        }
       
    }
}