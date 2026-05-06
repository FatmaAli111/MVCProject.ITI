using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCProject.ITI.DataAccessLayer.Entities;
using MVCProject.ITI.Serviceslayer;
using MVCProject.ITI.ViewModels.Settings;

namespace MVCProject.ITI.Controllers
{
    [Authorize]
    public class SettingsController : Controller
    {
        private readonly IUserSettingsService _userSettingsService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public SettingsController(
            IUserSettingsService userSettingsService,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userSettingsService = userSettingsService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = Guid.Parse(_userManager.GetUserId(User) ?? Guid.Empty.ToString());
            
            var viewModel = new SettingsViewModel
            {
                Profile = await _userSettingsService.GetProfileAsync(userId),
                VehicleInfo = await _userSettingsService.GetVehicleInfoAsync(userId),
                FavoriteTrips = await _userSettingsService.GetFavoriteTripsAsync(userId)
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProfile([FromBody] ProfileViewModel profile)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return Json(new { success = false, message = "Invalid data", errors = errors });
            }

            var userId = Guid.Parse(_userManager.GetUserId(User) ?? Guid.Empty.ToString());
            var result = await _userSettingsService.UpdateProfileAsync(userId, profile);

            return Json(new { success = result, message = result ? "Profile updated successfully" : "Failed to update profile" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateVehicleInfo(VehicleInfoViewModel vehicleInfo)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return Json(new { success = false, message = "Invalid data", errors = errors });
            }

            var userId = Guid.Parse(_userManager.GetUserId(User) ?? Guid.Empty.ToString());
            var result = await _userSettingsService.UpdateVehicleInfoAsync(userId, vehicleInfo);

            return Json(new { success = result, message = result ? "Vehicle info updated successfully" : "Failed to update vehicle info" });
        }

        [HttpGet]
        public async Task<IActionResult> GetMakes()
        {
            var makes = await _userSettingsService.GetMakesAsync();
            return Json(makes);
        }

        [HttpGet]
        public async Task<IActionResult> GetModels(string make)
        {
            var models = await _userSettingsService.GetModelsByMakeAsync(make);
            return Json(models);
        }

        [HttpGet]
        public async Task<IActionResult> GetYears(string make, string model)
        {
            var years = await _userSettingsService.GetYearsByMakeAndModelAsync(make, model);
            return Json(years);
        }

        [HttpGet]
        public async Task<IActionResult> GetCarModel(string make, string model, int year)
        {
            var carModel = await _userSettingsService.GetCarModelAsync(make, model, year);
            if (carModel == null)
                return Json(new { success = false });

            return Json(new { success = true, wltpMixed = carModel.WltpMixed, fuelType = carModel.FuelType.ToString() });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAccount()
        {
            var userId = Guid.Parse(_userManager.GetUserId(User) ?? Guid.Empty.ToString());
            var result = await _userSettingsService.DeleteUserAccountAsync(userId);

            if (result)
            {
                await _signInManager.SignOutAsync();
                return Json(new { success = true, message = "Account deleted successfully", redirectUrl = Url.Action("Index", "Home") });
            }

            return Json(new { success = false, message = "Failed to delete account" });
        }
    }
}
