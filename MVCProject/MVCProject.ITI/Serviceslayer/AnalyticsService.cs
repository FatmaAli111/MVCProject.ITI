using Microsoft.EntityFrameworkCore;
using MVCProject.ITI.DataAccessLayer.Data;
using MVCProject.ITI.ViewModels;

namespace MVCProject.ITI.Serviceslayer
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly ApplicationDbContext _context;

        public AnalyticsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AnalyticsViewModel> calcAnalytics(Guid userId)
        {

            var trips = await _context.Trips
                .Include(t => t.TripCostResult)
                .Where(t => t.UserId == userId)
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
                .Select(g => (double)g.Sum(t => t.TripCostResult?.TotalCost ?? 0))
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
            return vm;
        }
    }
}
