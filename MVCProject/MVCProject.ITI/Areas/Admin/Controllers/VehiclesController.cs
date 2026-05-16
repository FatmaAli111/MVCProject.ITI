using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCProject.ITI.Areas.Admin.ViewModels;
using MVCProject.ITI.DataAccessLayer.Data;
using MVCProject.ITI.Extensions;

namespace MVCProject.ITI.Areas.Admin.Controllers;

public class VehiclesController : AdminControllerBase
{
    private readonly ApplicationDbContext _context;

    public VehiclesController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(int page = 1, string? search = null)
    {
        var query = _context.Vehicles
            .AsNoTracking()
            .Include(v => v.User)
            .Include(v => v.CarModel)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim().ToLower();
            query = query.Where(v =>
                v.NickName.ToLower().Contains(term) ||
                (v.User.Email != null && v.User.Email.ToLower().Contains(term)) ||
                (v.CarModel != null && (v.CarModel.Make + " " + v.CarModel.Model).ToLower().Contains(term)));
        }

        var paged = await query
            .OrderByDescending(v => v.IsDefault)
            .ThenBy(v => v.NickName)
            .Select(v => new AdminVehicleListItemViewModel
            {
                Id = v.Id,
                OwnerEmail = v.User.Email ?? "",
                NickName = v.NickName,
                Make = v.CarModel != null ? v.CarModel.Make : "",
                Model = v.CarModel != null ? v.CarModel.Model : "",
                IsDefault = v.IsDefault,
                TripCount = v.Trips.Count
            })
            .ToPaginatedListAsync(page, 12);

        ViewBag.Search = search;
        return View(paged);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Guid id)
    {
        var vehicle = await _context.Vehicles
            .Include(v => v.Trips)
            .FirstOrDefaultAsync(v => v.Id == id);

        if (vehicle is null)
            return NotFound();

        if (vehicle.Trips.Any())
        {
            SetError("Cannot delete a vehicle that has trip history. Delete related trips first.");
            return RedirectToAction(nameof(Index));
        }

        var profiles = await _context.FuelEfficiencyProfiles
            .Where(f => f.VehicleId == id)
            .ToListAsync();

        _context.FuelEfficiencyProfiles.RemoveRange(profiles);
        _context.Vehicles.Remove(vehicle);
        await _context.SaveChangesAsync();

        SetSuccess("Vehicle deleted.");
        return RedirectToAction(nameof(Index));
    }
}
