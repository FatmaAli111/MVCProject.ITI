using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVCProject.ITI.DataAccessLayer.Entities;

namespace MVCProject.ITI.DataAccessLayer.Configurations
{
    public class VehicleConfiguration: IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder) {
            // Set default value for Vehicle.Id to use sequential GUIDs for better performance
            builder.Property(v => v.Id)
                   .HasDefaultValueSql("NEWSEQUENTIALID()");
            // Link Vehicle to the User
            builder.HasOne(v => v.User)
                   .WithMany(u => u.Vehicles)
                   .HasForeignKey(v => v.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(v => v.CarModel)
            .WithMany(cm => cm.Vehicles)
            .HasForeignKey(v => v.CarModelId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
                new Vehicle
                {
                    Id = Guid.Parse("f9b5a7a9-2f22-4a7b-a454-077a28424294"),
                    UserId = Guid.Parse("46686121-d1c1-4796-993d-82d2a45a6660"),
                    CarModelId = Guid.Parse("13735163-952a-466d-8e7c-87d3dfa7263b"),
                    NickName = "Admin's Corolla",
                    PassengerCapacity = 5
                }
            );
        }
    }
}
