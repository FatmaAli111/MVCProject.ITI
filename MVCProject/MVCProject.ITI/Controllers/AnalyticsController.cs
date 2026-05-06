using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCProject.ITI.DataAccessLayer.Data;
using MVCProject.ITI.DataAccessLayer.Entities;
using MVCProject.ITI.ViewModels;

namespace MVCProject.ITI.Controllers
{
    public class AnalyticsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        public AnalyticsController(UserManager<ApplicationUser> userManager , ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public async Task<IActionResult> Analytics()
        {
            var userId = _userManager.GetUserId(User);

            var trips = await _context.Trips
                .Include(t => t.TripCostResult)
                .Where(t => t.UserId.ToString() == userId)
                .ToListAsync();

            var vm = new AnalyticsViewModel();

            if (trips.Any())
            {
                vm.TripCount = trips.Count;
                vm.TotalDistance = trips.Sum(t => t.DistanceKm);
                vm.TotalSpent = trips.Sum(t => t.TripCostResult?.TotalCost ?? 0);
                vm.FuelCost = trips.Sum(t => t.TripCostResult?.FuelConsumed ?? 0);

                vm.Emissions = vm.TotalDistance * 0.131;

                vm.MonthlySpending = trips
                .GroupBy(t => t.TripDate.Month)
                .OrderBy(g => g.Key)
                .Select(g => (double)g.Sum(t => t.TripCostResult!.TotalCost))
                .ToList();
                
                
                           vm.MonthlyDistance = trips
                .GroupBy(t => t.TripDate.Month)
                .OrderBy(g => g.Key)
                .Select(g => (double)g.Sum(t => t.DistanceKm))
                .ToList();
                
                vm.FuelPercentage = 73;
                vm.TollsPercentage = 17;
                vm.MaintenancePercentage = 10;
            }

            return View(vm);
        }

    }
}
