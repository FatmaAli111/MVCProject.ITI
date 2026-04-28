namespace MVCProject.ITI.DataAccessLayer.Entities
{
    public class Trip
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; } = Guid.Empty;
        public Guid VehicleId { get; set; } = Guid.Empty;
        public string OriginPlaceId { get; set; } = string.Empty;
        public string OriginName { get; set; } = string.Empty;
        public string DestinationPlaceId { get; set; } = string.Empty;
        public string DestinationName { get; set; } = string.Empty;
        public float DistanceKm { get; set; }
        public int DurationMinutes { get; set; }
        public int PassengerCount { get; set; }
        public bool IsReturnTrip { get; set; }
        public bool IsFavorite { get; set; }
        public bool IsAcOn { get; set; }
        public DateTime TripDate { get; set; }
        public DateTime CreatedAt { get; set; }

        // Relationships ***********
        public ApplicationUser User { get; set; } = null!;
        public Vehicle Vehicle { get; set; } = null!;
        public TripCostResult? TripCostResult { get; set; } = null!;

    }
}
