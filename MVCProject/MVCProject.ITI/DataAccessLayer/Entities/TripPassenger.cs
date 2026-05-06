namespace MVCProject.ITI.DataAccessLayer.Entities
{
    public class TripPassenger
    {
        public Guid Id { get; set; }
        public Guid TripId { get; set; }
        public string Name { get; set; } = string.Empty;
        public float ShareAmount { get; set; }
        public float SharePercentage { get; set; }
        public Trip Trip { get; set; } = null!;
    }
}
