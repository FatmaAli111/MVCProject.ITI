using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcProject.iti.DataAccessLayer.Repository.GenericRepo;
using MVCProject.ITI.Areas.Admin.ViewModels;
using MVCProject.ITI.DataAccessLayer.Data;
using MVCProject.ITI.DataAccessLayer.Entities;
using MVCProject.ITI.Extensions;

namespace MVCProject.ITI.Areas.Admin.Controllers;

public class CarModelsController : AdminControllerBase
{
    private readonly IGenericRepository<CarModel> _repo;
    private readonly ApplicationDbContext _context;

    public CarModelsController(IGenericRepository<CarModel> repo, ApplicationDbContext context)
    {
        _repo = repo;
        _context = context;
    }

    public async Task<IActionResult> Index(int page = 1, string? search = null)
    {
        var query = _repo.GetTableNoTracking();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim().ToLower();
            query = query.Where(c =>
                c.Make.ToLower().Contains(term) ||
                c.Model.ToLower().Contains(term));
        }

        var paged = await query
            .OrderBy(c => c.Make)
            .ThenBy(c => c.Model)
            .Select(c => new AdminCarModelViewModel
            {
                Id = c.Id,
                Make = c.Make,
                Model = c.Model,
                Year = c.Year,
                FuelType = c.FuelType,
                WltpMixed = c.WltpMixed,
                TankCapacity = c.TankCapacity,
                BatteryCapacity = c.BatteryCapacity
            })
            .ToPaginatedListAsync(page, 12);

        ViewBag.Search = search;
        return View(paged);
    }

    public IActionResult Create() => View("Form", new AdminCarModelViewModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(AdminCarModelViewModel model)
    {
        if (!ModelState.IsValid)
            return View("Form", model);

        var entity = MapToEntity(model);
        _repo.Add(entity);
        _repo.SaveChanges();

        SetSuccess("Car model created.");
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Edit(Guid id)
    {
        var entity = _repo.GetById(id);
        if (entity is null)
            return NotFound();

        return View("Form", MapToViewModel(entity));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(AdminCarModelViewModel model)
    {
        if (!model.Id.HasValue)
            return NotFound();

        if (!ModelState.IsValid)
            return View("Form", model);

        var entity = _repo.GetById(model.Id.Value);
        if (entity is null)
            return NotFound();

        entity.Make = model.Make.Trim();
        entity.Model = model.Model.Trim();
        entity.Year = model.Year;
        entity.FuelType = model.FuelType;
        entity.WltpMixed = model.WltpMixed;
        entity.TankCapacity = model.TankCapacity;
        entity.BatteryCapacity = model.BatteryCapacity;

        _repo.Update(entity);
        _repo.SaveChanges();

        SetSuccess("Car model updated.");
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Guid id)
    {
        var entity = _repo.GetById(id);
        if (entity is null)
            return NotFound();

        var hasVehicles = await _context.Vehicles.AnyAsync(v => v.CarModelId == id);

        if (hasVehicles)
        {
            SetError("Cannot delete a car model linked to user vehicles.");
            return RedirectToAction(nameof(Index));
        }

        _repo.Delete(entity);
        _repo.SaveChanges();
        SetSuccess("Car model deleted.");
        return RedirectToAction(nameof(Index));
    }

    private static CarModel MapToEntity(AdminCarModelViewModel model) => new()
    {
        Make = model.Make.Trim(),
        Model = model.Model.Trim(),
        Year = model.Year,
        FuelType = model.FuelType,
        WltpMixed = model.WltpMixed,
        TankCapacity = model.TankCapacity,
        BatteryCapacity = model.BatteryCapacity
    };

    private static AdminCarModelViewModel MapToViewModel(CarModel entity) => new()
    {
        Id = entity.Id,
        Make = entity.Make,
        Model = entity.Model,
        Year = entity.Year,
        FuelType = entity.FuelType,
        WltpMixed = entity.WltpMixed,
        TankCapacity = entity.TankCapacity,
        BatteryCapacity = entity.BatteryCapacity
    };
}
