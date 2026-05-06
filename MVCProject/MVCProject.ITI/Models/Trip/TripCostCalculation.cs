namespace MVCProject.ITI.Models.Trip
{
    public class TripCostCalculation
    {
        public float TotalCost { get; set; }
        public float CostPerKm { get; set; }
        public float CostPerPassenger { get; set; }
        public float FuelConsumed { get; set; }

        public float WeatherMultiplier { get; set; }
        public float TrafficMultiplier { get; set; }

        public string WeatherCondition { get; set; } = string.Empty;
        public string TrafficCondition { get; set; } = string.Empty;

        public List<RouteOption> AvailableRoutes { get; set; } = new();
        public RouteOption? SelectedRoute { get; set; }
    }
}
