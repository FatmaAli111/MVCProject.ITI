using MVCProject.ITI.DataAccessLayer.Entities;

namespace MVCProject.ITI.ViewModels.Settings
{
    public class FavoriteTripViewModel
    {
        public Guid Id { get; set; }
        public string OriginName { get; set; } = string.Empty;
        public string DestinationName { get; set; } = string.Empty;
        public float DistanceKm { get; set; }
        public FuelTypeEnum FuelType { get; set; }
        public int PassengerCount { get; set; }
        public DateTime TripDate { get; set; }
    }
}
