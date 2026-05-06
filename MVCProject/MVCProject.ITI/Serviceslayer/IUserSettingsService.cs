using MVCProject.ITI.DataAccessLayer.Entities;
using MVCProject.ITI.ViewModels.Settings;

namespace MVCProject.ITI.Serviceslayer
{
    public interface IUserSettingsService
    {
        Task<ProfileViewModel> GetProfileAsync(Guid userId);
        Task<bool> UpdateProfileAsync(Guid userId, ProfileViewModel profile);
        
        Task<VehicleInfoViewModel?> GetVehicleInfoAsync(Guid userId);
        Task<bool> UpdateVehicleInfoAsync(Guid userId, VehicleInfoViewModel vehicleInfo);
        
        Task<List<FavoriteTripViewModel>> GetFavoriteTripsAsync(Guid userId);
        
        Task<List<string>> GetMakesAsync();
        Task<List<string>> GetModelsByMakeAsync(string make);
        Task<List<int>> GetYearsByMakeAndModelAsync(string make, string model);
        Task<CarModel?> GetCarModelAsync(string make, string model, int year);
        
        Task<bool> DeleteUserAccountAsync(Guid userId);
    }
}
