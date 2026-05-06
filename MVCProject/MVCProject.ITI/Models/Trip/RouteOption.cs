namespace MVCProject.ITI.Models.Trip
{
    public class RouteOption
    {
        public string Summary { get; set; } = string.Empty;
        public float DistanceKm { get; set; }
        public int DurationMinutes { get; set; }
        public string TrafficCondition { get; set; } = "Medium";
    }
}
