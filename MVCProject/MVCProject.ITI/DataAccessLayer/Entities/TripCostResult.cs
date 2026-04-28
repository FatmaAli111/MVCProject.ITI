namespace MVCProject.ITI.DataAccessLayer.Entities
{
    public class TripCostResult
    {
        public Guid Id { get; set; }
        public Guid TripId { get; set; }
        public Guid FuelPriceId { get; set; }
        public float FuelConsumed { get; set; }
        public float TotalCost { get; set; } = 0;
        public float CostPerKm { get; set; } = 0;
        public float CostPerPassenger { get; set; } = 0;
        public DateTime CalculatedAt { get; set; }

        // Relationships ***********
        public Trip Trip { get; set; } = null!;
        public FuelPrice FuelPrice { get; set; } = null!;

    }
}
