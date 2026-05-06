namespace MVCProject.ITI.ViewModels.Settings
{
    public class SettingsViewModel
    {
        public ProfileViewModel Profile { get; set; } = new();
        public VehicleInfoViewModel? VehicleInfo { get; set; }
        public List<FavoriteTripViewModel> FavoriteTrips { get; set; } = new();
    }
}
