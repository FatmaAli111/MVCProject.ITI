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


        }
    }
}
