using Microsoft.AspNetCore.Mvc;
using MvcProject.iti.DataAccessLayer.Repository.GenericRepo;
using MVCProject.ITI.Areas.Admin.ViewModels;
using MVCProject.ITI.DataAccessLayer.Entities;
using MVCProject.ITI.Extensions;

namespace MVCProject.ITI.Areas.Admin.Controllers;

public class FuelPricesController : AdminControllerBase
{
    private readonly IGenericRepository<FuelPrice> _repo;

    public FuelPricesController(IGenericRepository<FuelPrice> repo)
    {
        _repo = repo;
    }

    public async Task<IActionResult> Index(int page = 1)
    {
        var paged = await _repo.GetTableNoTracking()
            .OrderByDescending(f => f.RecordedDate)
            .ThenBy(f => f.FuelType)
            .Select(f => new AdminFuelPriceViewModel
            {
                Id = f.Id,
                FuelType = f.FuelType,
                Region = f.Region,
                PricePerUnit = f.PricePerUnit,
                Currency = f.Currency,
                RecordedDate = f.RecordedDate
            })
            .ToPaginatedListAsync(page, 12);

        return View(paged);
    }

    public IActionResult Create() => View("Form", new AdminFuelPriceViewModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(AdminFuelPriceViewModel model)
    {
        if (!ModelState.IsValid)
            return View("Form", model);

        _repo.Add(MapToEntity(model));
        _repo.SaveChanges();

        SetSuccess("Fuel price added.");
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
    public IActionResult Edit(AdminFuelPriceViewModel model)
    {
        if (!model.Id.HasValue)
            return NotFound();

        if (!ModelState.IsValid)
            return View("Form", model);

        var entity = _repo.GetById(model.Id.Value);
        if (entity is null)
            return NotFound();

        entity.FuelType = model.FuelType;
        entity.Region = model.Region.Trim();
        entity.PricePerUnit = model.PricePerUnit;
        entity.Currency = model.Currency.Trim();
        entity.RecordedDate = model.RecordedDate;

        _repo.Update(entity);
        _repo.SaveChanges();

        SetSuccess("Fuel price updated.");
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(Guid id)
    {
        var entity = _repo.GetById(id);
        if (entity is null)
            return NotFound();

        try
        {
            _repo.Delete(entity);
            _repo.SaveChanges();
            SetSuccess("Fuel price deleted.");
        }
        catch
        {
            SetError("Cannot delete this fuel price because it is used in trip cost calculations.");
        }

        return RedirectToAction(nameof(Index));
    }

    private static FuelPrice MapToEntity(AdminFuelPriceViewModel model) => new()
    {
        FuelType = model.FuelType,
        Region = model.Region.Trim(),
        PricePerUnit = model.PricePerUnit,
        Currency = model.Currency.Trim(),
        RecordedDate = model.RecordedDate
    };

    private static AdminFuelPriceViewModel MapToViewModel(FuelPrice entity) => new()
    {
        Id = entity.Id,
        FuelType = entity.FuelType,
        Region = entity.Region,
        PricePerUnit = entity.PricePerUnit,
        Currency = entity.Currency,
        RecordedDate = entity.RecordedDate
    };
}
