using Microsoft.AspNetCore.Mvc;
using MVCProject.ITI.DataAccessLayer.Entities;
using MvcProject.iti.DataAccessLayer.Repository.GenericRepo;
using MVCProject.ITI.Serviceslayer;
using MVCProject.ITI.Models;
using Microsoft.AspNetCore.Identity;

namespace MVCProject.ITI.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IRecentTripService _recentTripService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly VehicleService _vechileService;

        public DashboardController(IRecentTripService recentTripService
            ,UserManager<ApplicationUser> userManager, VehicleService vechileService)
        {
            _recentTripService = recentTripService;
            _userManager = userManager;
            _vechileService = vechileService;
        }

        public async Task<IActionResult> DashboardAsync()
        {

            try
            {
                ApplicationUser user = await _userManager.GetUserAsync(User);
                if (user is null)
                    return RedirectToPage("/Account/Login", new { area = "Identity" });

                Guid id = user.Id;
                ViewBag.Vehicle = _vechileService.GetById(id);

                IEnumerable<TripCardViewModel> RecentTrips = await _recentTripService.GetRecentTrips(id);
                return View(RecentTrips);
            }
            catch(Exception ex)
            {
                return RedirectToAction("Error","Home",ex.Message);
            }
        }
        public IActionResult StartNewTrip()
        {
            return View();
        }
    }
}
