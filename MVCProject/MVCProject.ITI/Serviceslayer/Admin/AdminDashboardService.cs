using Microsoft.EntityFrameworkCore;
using MVCProject.ITI.Areas.Admin.ViewModels;
using MVCProject.ITI.DataAccessLayer.Data;

namespace MVCProject.ITI.Serviceslayer.Admin;

public class AdminDashboardService : IAdminDashboardService
{
    private readonly ApplicationDbContext _context;

    public AdminDashboardService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<AdminDashboardViewModel> GetDashboardAsync(CancellationToken cancellationToken = default)
    {
        var totalUsers = await _context.Users.CountAsync(cancellationToken);
        var totalTrips = await _context.Trips.CountAsync(cancellationToken);
        var totalVehicles = await _context.Vehicles.CountAsync(cancellationToken);
        var totalCarModels = await _context.Cars.CountAsync(cancellationToken);
        var totalFuelPrices = await _context.FuelPrices.CountAsync(cancellationToken);

        var revenue = await _context.TripCostResults.SumAsync(t => (decimal?)t.TotalCost, cancellationToken) ?? 0;
        var distance = await _context.Trips.SumAsync(t => (double?)t.DistanceKm, cancellationToken) ?? 0;

        var recentTrips = await _context.Trips
            .AsNoTracking()
            .Include(t => t.User)
            .Include(t => t.Vehicle)
            .OrderByDescending(t => t.CreatedAt)
            .Take(8)
            .Select(t => new AdminTripListItemViewModel
            {
                Id = t.Id,
                UserEmail = t.User.Email ?? "",
                UserFullName = t.User.FullName,
                OriginName = t.OriginName,
                DestinationName = t.DestinationName,
                DistanceKm = t.DistanceKm,
                TripDate = t.TripDate,
                CreatedAt = t.CreatedAt,
                PassengerCount = t.PassengerCount
            })
            .ToListAsync(cancellationToken);

        var recentUsers = await _context.Users
            .AsNoTracking()
            .OrderByDescending(u => u.Id)
            .Take(8)
            .Select(u => new AdminUserListItemViewModel
            {
                Id = u.Id,
                Email = u.Email ?? "",
                FullName = u.FullName,
                EmailConfirmed = u.EmailConfirmed,
                LockoutEnabled = u.LockoutEnabled,
                LockoutEnd = u.LockoutEnd
            })
            .ToListAsync(cancellationToken);

        return new AdminDashboardViewModel
        {
            TotalUsers = totalUsers,
            TotalTrips = totalTrips,
            TotalVehicles = totalVehicles,
            TotalCarModels = totalCarModels,
            TotalFuelPrices = totalFuelPrices,
            TotalRevenue = (float)revenue,
            TotalDistanceKm = (float)distance,
            RecentTrips = recentTrips,
            RecentUsers = recentUsers
        };
    }
}
