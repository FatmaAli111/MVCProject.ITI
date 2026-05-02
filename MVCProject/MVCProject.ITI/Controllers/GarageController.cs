using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCProject.ITI.DataAccessLayer.Entities;
using MVCProject.ITI.ViewModels;

public class GarageController : Controller
{
    private readonly VehicleService _vehicleService;
    private readonly CarModelService _carModelService;
    private readonly UserManager<ApplicationUser> _userManager;

    public GarageController(
        VehicleService vehicleService,
        CarModelService carModelService,
        UserManager<ApplicationUser> userManager)
    {
        _vehicleService = vehicleService;
        _carModelService = carModelService;
        _userManager = userManager;
    }

    public IActionResult Garage()
    {
        var vehicles = _vehicleService.GetAll();
        return View(vehicles);
    }

    [HttpGet]
    public IActionResult AddVehicle()
    {
        return View(new AddVechileViewModel());
    }

    [HttpPost]
    public IActionResult AddVehicle(AddVechileViewModel vm)
    {
        if (!ModelState.IsValid)
            return View(vm);

        var userId = Guid.Parse(_userManager.GetUserId(User));
        var model = new CarModel
        {
            Make = vm.Make,
            Model = vm.Model,
            Year = vm.Year,
            FuelType = vm.FuelType,
            WltpMixed = vm.WltpMixed,
            TankCapacity = vm.TankCapacity
        };

        _carModelService.Add(model);

        var vehicle = new Vehicle
        {
            NickName = vm.NickName,
            ColorHex = vm.ColorHex,
            CarModelId = model.Id,
            UserId = userId
        };

        _vehicleService.Add(vehicle);

        return RedirectToAction("Garage");
    }

    [HttpGet]
    public IActionResult Edit(Guid id)
    {
        var vehicle = _vehicleService.GetById(id);
        var model = _carModelService.GetById(vehicle.CarModelId);

        var vm = new AddVechileViewModel
        {
            VehicleId = vehicle.Id,
            CarModelId = model.Id,
            NickName = vehicle.NickName,
            ColorHex = vehicle.ColorHex,
            Make = model.Make,
            Model = model.Model,
            Year = model.Year,
            FuelType = model.FuelType,
            WltpMixed = model.WltpMixed,
            TankCapacity = model.TankCapacity,
            BatteryCapacity = model.BatteryCapacity
        };

        return View("AddVehicle", vm);
    }


    [HttpPost]
    public IActionResult Edit(AddVechileViewModel vm)
    {
        if (!ModelState.IsValid)
            return View("AddVehicle", vm);

        var model = _carModelService.GetById(vm.CarModelId);

        model.Make = vm.Make;
        model.Model = vm.Model;
        model.Year = vm.Year;
        model.FuelType = vm.FuelType;
        model.WltpMixed = vm.WltpMixed;
        model.TankCapacity = vm.TankCapacity;

        _carModelService.Update(model);

        var vehicle = _vehicleService.GetById(vm.VehicleId);
        vehicle.NickName = vm.NickName;
        vehicle.ColorHex = vm.ColorHex;

        _vehicleService.Update(vehicle);

        return RedirectToAction("Garage");
    }
    [HttpPost]
    public IActionResult DeleteVehicle(Guid id)
    {
        var vehicle = _vehicleService.GetById(id);

        if (vehicle == null)
            return RedirectToAction("Garage");
        var model = _carModelService.GetById(vehicle.CarModelId);
        _vehicleService.Delete(vehicle.Id);
        if (model != null)
            _carModelService.Delete(model);
        return RedirectToAction("Garage");
    }

    [HttpPost]
    public IActionResult SetDefault(Guid id)
    {
        var userIdString = _userManager.GetUserId(User);
        var userId = Guid.Parse(userIdString);

        _vehicleService.SetDefaultVehicle(id, userId);

        return RedirectToAction("Garage");
    }
    ///////////////////////////////////////////////////////
    
}
