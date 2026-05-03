using Microsoft.AspNetCore.Identity;

namespace MVCProject.ITI.DataAccessLayer.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FullName { get; set; } = string.Empty;

        // Custom user class to add navigation properties
        public ApplicationUser() { }

        // Relationships ***********
        public ICollection<Vehicle> Vehicles { get; set; } = [];
        public ICollection<Trip> Trips { get; set; } = [];

    }
}
