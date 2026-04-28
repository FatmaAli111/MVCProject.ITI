namespace MVCProject.ITI.DataAccessLayer.Entities
{
    public class FuelPrice
    {
        public Guid Id { get; set; }
        public FuelTypeEnum FuelType { get; set; }
        public string Region { get; set; } = string.Empty;
        public float PricePerUnit { get; set; }
        public string Currency { get; set; } = "EGP";
        public DateTime RecordedDate { get; set; }

        // Relationships ***********
        public ICollection<TripCostResult> TripCostResults { get; set; } = [];
    }
}
