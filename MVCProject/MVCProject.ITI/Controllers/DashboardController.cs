using Microsoft.AspNetCore.Mvc;
using MVCProject.ITI.DataAccessLayer.Entities;
using MvcProject.iti.DataAccessLayer.Repository.GenericRepo;
using MVCProject.ITI.Serviceslayer;
using MVCProject.ITI.Models;

namespace MVCProject.ITI.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IRecentTripService _recentTripService;

        public DashboardController(IRecentTripService recentTripService)
        {
            _recentTripService = recentTripService;
        }

        public async Task<IActionResult> DashboardAsync()
        {
            IEnumerable<TripCardViewModel> RecentTrips =await _recentTripService.GetRecentTrips();
            return View(RecentTrips);
        }
    }
}
