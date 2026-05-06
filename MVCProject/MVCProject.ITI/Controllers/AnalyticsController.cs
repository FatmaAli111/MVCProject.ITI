using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCProject.ITI.DataAccessLayer.Data;
using MVCProject.ITI.DataAccessLayer.Entities;
using MVCProject.ITI.Serviceslayer;
using MVCProject.ITI.ViewModels;

namespace MVCProject.ITI.Controllers
{
    [Authorize]

    public class AnalyticsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAnalyticsService _analyticsService;
        public AnalyticsController(UserManager<ApplicationUser> userManager ,IAnalyticsService analyticsService)
        {
            _userManager = userManager;
            _analyticsService = analyticsService;
        }
        public async Task<IActionResult> Analytics()
        {
            var userId =await _userManager.GetUserAsync(User);
            var vm = await _analyticsService.calcAnalytics(userId.Id);
            return View(vm);
        }

    }
}
