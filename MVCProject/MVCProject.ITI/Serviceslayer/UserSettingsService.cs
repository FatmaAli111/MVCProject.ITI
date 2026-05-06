using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MvcProject.iti.DataAccessLayer.Repository.GenericRepo;
using MVCProject.ITI.DataAccessLayer.Entities;
using MVCProject.ITI.ViewModels.Settings;

namespace MVCProject.ITI.Serviceslayer
{
    public class UserSettingsService : IUserSettingsService
    {
        private readonly IGenericRepository<ApplicationUser> _userRepo;
        private readonly IGenericRepository<Vehicle> _vehicleRepo;
        private readonly IGenericRepository<CarModel> _carModelRepo;
        private readonly IGenericRepository<Trip> _tripRepo;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserSettingsService(
            IGenericRepository<ApplicationUser> userRepo,
            IGenericRepository<Vehicle> vehicleRepo,
            IGenericRepository<CarModel> carModelRepo,
            IGenericRepository<Trip> tripRepo,
            UserManager<ApplicationUser> userManager)
        {
            _userRepo = userRepo;
            _vehicleRepo = vehicleRepo;
            _carModelRepo = carModelRepo;
            _tripRepo = tripRepo;
            _userManager = userManager;
        }

        public async Task<ProfileViewModel> GetProfileAsync(Guid userId)
        {
            var user = await _userRepo.GetTableNoTracking()
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return new ProfileViewModel();

            return new ProfileViewModel
            {
                FullName = user.FullName,
                Email = user.Email ?? string.Empty,
                PhoneNumber = user.PhoneNumber
            };
        }

        public async Task<bool> UpdateProfileAsync(Guid userId, ProfileViewModel profile)
        {
            var user = await _userRepo.GetTableAsTracking()
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return false;

            user.FullName = profile.FullName;
            user.PhoneNumber = profile.PhoneNumber;

            _userRepo.Update(user);
            _userRepo.SaveChanges();

            return true;
        }

        public async Task<VehicleInfoViewModel?> GetVehicleInfoAsync(Guid userId)
        {
            var vehicle = await _vehicleRepo.GetTableNoTracking()
                .Include(v => v.CarModel)
                .FirstOrDefaultAsync(v => v.UserId == userId && v.IsDefault);

            if (vehicle == null)
            {
                var anyVehicle = await _vehicleRepo.GetTableNoTracking()
                    .Include(v => v.CarModel)
                    .FirstOrDefaultAsync(v => v.UserId == userId);

                if (anyVehicle == null)
                    return null;

                vehicle = anyVehicle;
            }

            if (vehicle.CarModel == null)
                return null;

            var makes = await GetMakesAsync();
            var models = await GetModelsByMakeAsync(vehicle.CarModel.Make);
            var years = await GetYearsByMakeAndModelAsync(vehicle.CarModel.Make, vehicle.CarModel.Model);

            return new VehicleInfoViewModel
            {
                VehicleId = vehicle.Id,
                Make = vehicle.CarModel.Make,
                Model = vehicle.CarModel.Model,
                Year = vehicle.CarModel.Year,
                PassengerCapacity = 5,
                FuelType = vehicle.CarModel.FuelType,
                WltpMixed = vehicle.CarModel.WltpMixed,
                CarNickname = vehicle.NickName,
                IsOverride = false,
                AvailableMakes = makes,
                AvailableModels = models,
                AvailableYears = years
            };
        }

        public async Task<bool> UpdateVehicleInfoAsync(Guid userId, VehicleInfoViewModel vehicleInfo)
        {
            var vehicle = await _vehicleRepo.GetTableAsTracking()
                .Include(v => v.CarModel)
                .FirstOrDefaultAsync(v => v.Id == vehicleInfo.VehicleId);

            if (vehicle == null)
                return false;

            vehicle.NickName = vehicleInfo.CarNickname;

            if (vehicle.CarModel != null)
            {
                vehicle.CarModel.Make = vehicleInfo.Make;
                vehicle.CarModel.Model = vehicleInfo.Model;
                vehicle.CarModel.Year = vehicleInfo.Year;
                vehicle.CarModel.FuelType = vehicleInfo.FuelType;
                
                if (vehicleInfo.IsOverride)
                {
                    vehicle.CarModel.WltpMixed = vehicleInfo.WltpMixed;
                }
            }

            _vehicleRepo.Update(vehicle);
            _vehicleRepo.SaveChanges();

            return true;
        }

        public async Task<List<FavoriteTripViewModel>> GetFavoriteTripsAsync(Guid userId)
        {
            var trips = await _tripRepo.GetTableNoTracking()
                .Include(t => t.Vehicle)
                    .ThenInclude(v => v.CarModel)
                .Where(t => t.UserId == userId && t.IsFavorite)
                .OrderByDescending(t => t.TripDate)
                .ToListAsync();

            return trips.Select(t => new FavoriteTripViewModel
            {
                Id = t.Id,
                OriginName = t.OriginName,
                DestinationName = t.DestinationName,
                DistanceKm = t.DistanceKm,
                FuelType = t.Vehicle?.CarModel?.FuelType ?? FuelTypeEnum.Gasoline,
                PassengerCount = t.PassengerCount,
                TripDate = t.TripDate
            }).ToList();
        }

        public async Task<List<string>> GetMakesAsync()
        {
            return await _carModelRepo.GetTableNoTracking()
                .Select(cm => cm.Make)
                .Distinct()
                .OrderBy(m => m)
                .ToListAsync();
        }

        public async Task<List<string>> GetModelsByMakeAsync(string make)
        {
            return await _carModelRepo.GetTableNoTracking()
                .Where(cm => cm.Make == make)
                .Select(cm => cm.Model)
                .Distinct()
                .OrderBy(m => m)
                .ToListAsync();
        }

        public async Task<List<int>> GetYearsByMakeAndModelAsync(string make, string model)
        {
            return await _carModelRepo.GetTableNoTracking()
                .Where(cm => cm.Make == make && cm.Model == model)
                .Select(cm => cm.Year)
                .Distinct()
                .OrderBy(y => y)
                .ToListAsync();
        }

        public async Task<CarModel?> GetCarModelAsync(string make, string model, int year)
        {
            return await _carModelRepo.GetTableNoTracking()
                .FirstOrDefaultAsync(cm => cm.Make == make && cm.Model == model && cm.Year == year);
        }

        public async Task<bool> DeleteUserAccountAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                return false;

            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }
    }
}
