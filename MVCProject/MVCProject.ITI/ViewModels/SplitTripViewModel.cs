namespace MVCProject.ITI.ViewModels
{
    public class SplitTripViewModel
    {
        public Guid TripId { get; set; }
        public string OriginName { get; set; } = string.Empty;
        public string DestinationName { get; set; } = string.Empty;
        public float DistanceKm { get; set; }
        public DateTime TripDate { get; set; }

        public float TotalCost { get; set; }
        public float FuelConsumed { get; set; }
        public float CostPerKm { get; set; }

        public List<PassengerSplitVM> Passengers { get; set; } = new();

        public float DriverShare { get; set; }
        public float DriverPercentage { get; set; }
        public float DriverAmount { get; set; }
        public string Mode { get; set; }
    }
}
