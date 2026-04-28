using System.Diagnostics.CodeAnalysis;

namespace MVCProject.ITI.DataAccessLayer.Entities
{
    
    public class Vehicle
    {
        // This table is for storing the user vehicles information
        public Guid Id { get; set; }
        public Guid UserId { get; set; } = Guid.Empty;
        public Guid CarModelId { get; set; } = Guid.Empty;
        public string NickName { get; set; } = string.Empty;
        
        public int PassengerCapacity { get; set; }

        // Relationships ***********
        public ApplicationUser User { get; set; } = null!;
        // A vehicle might not have a car model
        // because we can't cover all possible models in the world,
        // so it's nullable
        public CarModel? CarModel { get; set; }
        public ICollection<Trip> Trips { get; set; } = [];
        public ICollection<FuelEfficiencyProfile> FuelEfficiencyProfiles { get; set; } = [];

    }
}
