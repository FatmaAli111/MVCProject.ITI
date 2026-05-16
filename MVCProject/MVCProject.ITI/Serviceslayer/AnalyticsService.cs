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

            if (!trips.Any())
                return vm;

            vm.TripCount = trips.Count;
            vm.TotalDistance = trips.Sum(t => t.DistanceKm);
            vm.TotalSpent = trips.Sum(t => t.TripCostResult?.TotalCost ?? 0);

            var fuelCostTotal = trips.Sum(t => (t.TripCostResult?.FuelConsumed ?? 0) * 18.5);
            var maintenanceTotal = trips.Sum(t => t.DistanceKm * 0.75);
            vm.FuelCost = fuelCostTotal;

            vm.Emissions = vm.TotalDistance * 0.131;

            if (vm.TotalSpent > 0)
            {
                vm.FuelPercentage = Math.Round(fuelCostTotal / vm.TotalSpent * 100, 1);
                vm.MaintenancePercentage = Math.Round(maintenanceTotal / vm.TotalSpent * 100, 1);
                var remainder = 100 - vm.FuelPercentage - vm.MaintenancePercentage;
                vm.TollsPercentage = Math.Max(0, Math.Round(remainder, 1));
            }

            var monthStart = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1).AddMonths(-11);
            var monthBuckets = Enumerable.Range(0, 12)
                .Select(i => monthStart.AddMonths(i))
                .ToList();

            vm.MonthlySpending = monthBuckets.Select(m => new MonthlyChartPoint
            {
                Key = m.ToString("MMM yy"),
                Value = trips
                    .Where(t => t.TripDate.Year == m.Year && t.TripDate.Month == m.Month)
                    .Sum(t => (double)(t.TripCostResult?.TotalCost ?? 0))
            }).ToList();

            vm.MonthlyDistance = monthBuckets.Select(m => new MonthlyChartPoint
            {
                Key = m.ToString("MMM yy"),
                Value = trips
                    .Where(t => t.TripDate.Year == m.Year && t.TripDate.Month == m.Month)
                    .Sum(t => t.DistanceKm)
            }).ToList();

            return vm;
        }
    }
}
