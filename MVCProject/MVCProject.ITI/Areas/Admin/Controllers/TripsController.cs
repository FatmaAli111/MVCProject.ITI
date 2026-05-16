using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCProject.ITI.Areas.Admin.ViewModels;
using MVCProject.ITI.DataAccessLayer.Data;
using MVCProject.ITI.Extensions;

namespace MVCProject.ITI.Areas.Admin.Controllers;

public class TripsController : AdminControllerBase
{
    private readonly ApplicationDbContext _context;

    public TripsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(int page = 1, string? search = null)
    {
        var query = _context.Trips
            .AsNoTracking()
            .Include(t => t.User)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim().ToLower();
            query = query.Where(t =>
                t.OriginName.ToLower().Contains(term) ||
                t.DestinationName.ToLower().Contains(term) ||
                (t.User.Email != null && t.User.Email.ToLower().Contains(term)));
        }

        var paged = await query
            .OrderByDescending(t => t.CreatedAt)
            .Select(t => new AdminTripListItemViewModel
            {
                Id = t.Id,
                UserEmail = t.User.Email ?? "",
                UserFullName = t.User.FullName,
                OriginName = t.OriginName,
                DestinationName = t.DestinationName,
                DistanceKm = t.DistanceKm,
                PassengerCount = t.PassengerCount,
                TripDate = t.TripDate,
                CreatedAt = t.CreatedAt,
                IsFavorite = t.IsFavorite
            })
            .ToPaginatedListAsync(page, 12);

        ViewBag.Search = search;
        return View(paged);
    }

    public async Task<IActionResult> Details(Guid id)
    {
        var trip = await _context.Trips
            .AsNoTracking()
            .Include(t => t.User)
            .Include(t => t.Vehicle)
            .Include(t => t.TripCostResult)
            .Include(t => t.TripPassengers)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (trip is null)
            return NotFound();

        var vm = new AdminTripDetailsViewModel
        {
            Id = trip.Id,
            UserEmail = trip.User.Email ?? "",
            UserFullName = trip.User.FullName,
            VehicleName = trip.Vehicle?.NickName ?? "—",
            OriginName = trip.OriginName,
            DestinationName = trip.DestinationName,
            DistanceKm = trip.DistanceKm,
            DurationMinutes = trip.DurationMinutes,
            PassengerCount = trip.PassengerCount,
            IsReturnTrip = trip.IsReturnTrip,
            IsFavorite = trip.IsFavorite,
            IsAcOn = trip.IsAcOn,
            TripDate = trip.TripDate,
            CreatedAt = trip.CreatedAt,
            TotalCost = trip.TripCostResult?.TotalCost,
            CostPerPassenger = trip.TripCostResult?.CostPerPassenger,
            PassengerNames = trip.TripPassengers.Select(p => p.Name).ToList()
        };

        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Guid id)
    {
        var trip = await _context.Trips
            .Include(t => t.TripPassengers)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (trip is null)
            return NotFound();

        _context.Trips.Remove(trip);
        await _context.SaveChangesAsync();
        SetSuccess("Trip deleted.");
        return RedirectToAction(nameof(Index));
    }
}
