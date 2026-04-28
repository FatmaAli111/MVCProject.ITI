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
        }
    }
}
