using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVCProject.ITI.DataAccessLayer.Entities;

namespace MVCProject.ITI.DataAccessLayer.Configurations
{
    public class TripConfiguration: IEntityTypeConfiguration<Trip>
    {
        public void Configure(EntityTypeBuilder<Trip> builder) {
            // Set default value for Trip.Id to use sequential GUIDs for better performance
            builder.Property(t => t.Id)
                   .HasDefaultValueSql("NEWSEQUENTIALID()");

            // Link Trip to the User
            builder.HasOne(t => t.User)
                   .WithMany(u => u.Trips)
                   .HasForeignKey(t => t.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Link Trip to the Vehicle
            builder.HasOne(t => t.Vehicle)
               .WithMany(v => v.Trips)
               .HasForeignKey(t => t.VehicleId)
               .OnDelete(DeleteBehavior.Restrict); // can not delete a vehicle if it has trips associated with it

            builder.HasData(
                new Trip
                {
                    Id = Guid.Parse("39542a3a-2a4c-4737-9755-e7a685764d26"),
                    UserId = Guid.Parse("46686121-d1c1-4796-993d-82d2a45a6660"),
                    VehicleId = Guid.Parse("f9b5a7a9-2f22-4a7b-a454-077a28424294"),
                    OriginName = "Cairo",
                    DestinationName = "Alexandria",
                    DistanceKm = 220f,
                    DurationMinutes = 180,
                    PassengerCount = 2,
                    TripDate = new DateTime(2024, 1, 1),
                    CreatedAt = new DateTime(2024, 1, 1)
                }
            );


        }
    }
}
