using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MvcProject.iti.DataAccessLayer.Repository.GenericRepo;
using MVCProject.ITI.DataAccessLayer.Entities;
using MVCProject.ITI.Models;
using MVCProject.ITI.Serviceslayer;
using MVCProject.ITI.ViewModels;
using System;

namespace MVCProject.ITI.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly IRecentTripService _recentTripService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly VehicleService _vechileService;
        private readonly IAnalyticsService _analyticsService;

        public DashboardController(IRecentTripService recentTripService
            ,UserManager<ApplicationUser> userManager
            , VehicleService vechileService
            , IAnalyticsService analyticsService)
        {
            _recentTripService = recentTripService;
            _userManager = userManager;
            _vechileService = vechileService;
            _analyticsService = analyticsService;
        }
        public async Task<IActionResult> DashboardAsync()
        {

            try
            {
                ApplicationUser user = await _userManager.GetUserAsync(User);
                if (user is null)
                    return Redirect("/Identity/Account/Login");

                Guid id = user.Id;
                ViewBag.Vehicle =await _vechileService.GetDefaultVehicleAsync(id);
                ViewBag.analytics= await _analyticsService.calcAnalytics(id);

                IEnumerable<TripCardViewModel> RecentTrips = await _recentTripService.GetRecentTrips(id);
                return View(RecentTrips);
            }
            catch(Exception ex)
            {
                return RedirectToAction("Error","Home",ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> StartNewTripAsync()
        {
            try
            {
                ApplicationUser user = await _userManager.GetUserAsync(User);
                if (user is null)
                    return RedirectToPage("/Account/Login", new { area = "Identity" });

                Guid id = user.Id;
                ViewBag.Vehicle =await _vechileService.GetDefaultVehicleAsync(id);

                IEnumerable<TripCardViewModel> AllTrips = await _recentTripService.GetAllTrips();
                ViewData["AllTrips"] = AllTrips;
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { message = ex.Message });
            }
        }
        [HttpPost]
        public async Task<IActionResult> StartNewTripAsync(NewTripViewModel newTripViewModel)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    ApplicationUser user = await _userManager.GetUserAsync(User);
                    if (user is null)
                        return RedirectToPage("/Account/Login", new { area = "Identity" });

                    Guid id = user.Id;
                    var vehicle=await _vechileService.GetDefaultVehicleAsync(id);
                    newTripViewModel.UserId = id;
                    if(vehicle!=null)
                    newTripViewModel.VehicleId = vehicle.Id;
                    else
                        return RedirectToAction("Error", "Home", new { message = "You Should Set Vehicle" });
                    await _recentTripService.AddTrip(newTripViewModel);

                }
                catch (Exception ex)
                {
                    //return RedirectToAction("Error", "Home", new { message = ex.Message });
                    throw;
                }
            }
            return RedirectToAction("History", "Trip");
        }

    }
}
