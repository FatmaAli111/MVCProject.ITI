namespace MVCProject.ITI.DataAccessLayer.Entities
{
    public class SavePassengersRequest
    {
        public Guid TripId { get; set; }
        public List<TripPassenger> Passengers { get; set; } = new();

    }
}
