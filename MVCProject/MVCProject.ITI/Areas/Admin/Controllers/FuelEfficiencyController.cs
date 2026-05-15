using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcProject.iti.DataAccessLayer.Repository.GenericRepo;
using MVCProject.ITI.Areas.Admin.ViewModels;
using MVCProject.ITI.DataAccessLayer.Entities;
using MVCProject.ITI.DataAccessLayer.Data;
using MVCProject.ITI.Extensions;

namespace MVCProject.ITI.Areas.Admin.Controllers;

public class FuelEfficiencyController : AdminControllerBase
{
    private readonly IGenericRepository<FuelEfficiencyProfile> _repo;
    private readonly ApplicationDbContext _context;

    public FuelEfficiencyController(
        IGenericRepository<FuelEfficiencyProfile> repo,
        ApplicationDbContext context)
    {
        _repo = repo;
        _context = context;
    }

    public async Task<IActionResult> Index(int page = 1)
    {
        var paged = await _repo.GetTableNoTracking()
            .Include(f => f.Vehicle)
            .ThenInclude(v => v.User)
            .OrderByDescending(f => f.ConsumptionRate)
            .Select(f => new AdminFuelEfficiencyViewModel
            {
                Id = f.Id,
                VehicleId = f.VehicleId,
                ConsumptionRate = f.ConsumptionRate,
                Unit = f.Unit,
                VehicleLabel = f.Vehicle.NickName + " (" + (f.Vehicle.User.Email ?? "user") + ")"
            })
            .ToPaginatedListAsync(page, 12);

        return View(paged);
    }

    public async Task<IActionResult> Create()
    {
        await LoadVehiclesAsync();
        return View("Form", new AdminFuelEfficiencyViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(AdminFuelEfficiencyViewModel model)
    {
        if (!ModelState.IsValid)
        {
            await LoadVehiclesAsync();
            return View("Form", model);
        }

        _repo.Add(new FuelEfficiencyProfile
        {
            VehicleId = model.VehicleId,
            ConsumptionRate = model.ConsumptionRate,
            Unit = model.Unit.Trim()
        });
        _repo.SaveChanges();

        SetSuccess("Fuel efficiency profile created.");
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        var entity = _repo.GetTableNoTracking()
            .Include(f => f.Vehicle)
            .FirstOrDefault(f => f.Id == id);

        if (entity is null)
            return NotFound();

        await LoadVehiclesAsync();
        return View("Form", new AdminFuelEfficiencyViewModel
        {
            Id = entity.Id,
            VehicleId = entity.VehicleId,
            ConsumptionRate = entity.ConsumptionRate,
            Unit = entity.Unit
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(AdminFuelEfficiencyViewModel model)
    {
        if (!model.Id.HasValue)
            return NotFound();

        if (!ModelState.IsValid)
        {
            await LoadVehiclesAsync();
            return View("Form", model);
        }

        var entity = _repo.GetById(model.Id.Value);
        if (entity is null)
            return NotFound();

        entity.VehicleId = model.VehicleId;
        entity.ConsumptionRate = model.ConsumptionRate;
        entity.Unit = model.Unit.Trim();

        _repo.Update(entity);
        _repo.SaveChanges();

        SetSuccess("Fuel efficiency profile updated.");
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(Guid id)
    {
        var entity = _repo.GetById(id);
        if (entity is null)
            return NotFound();

        _repo.Delete(entity);
        _repo.SaveChanges();
        SetSuccess("Profile deleted.");
        return RedirectToAction(nameof(Index));
    }

    private async Task LoadVehiclesAsync()
    {
        ViewBag.Vehicles = await _context.Vehicles
            .AsNoTracking()
            .Include(v => v.User)
            .OrderBy(v => v.NickName)
            .Select(v => new SelectListItem
            {
                Value = v.Id.ToString(),
                Text = v.NickName + " — " + (v.User.Email ?? v.UserId.ToString())
            })
            .ToListAsync();
    }
}
