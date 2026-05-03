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

        public DashboardController(IRecentTripService recentTripService,UserManager<ApplicationUser> userManager)
        {
            _recentTripService = recentTripService;
            _userManager = userManager;
        }

        public async Task<IActionResult> DashboardAsync()
        {
            try
            {
                ApplicationUser user = await _userManager.GetUserAsync(User);
                if (user is null)
                    return RedirectToPage("/Account/Login", new { area = "Identity" });

                Guid id = user.Id;
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
