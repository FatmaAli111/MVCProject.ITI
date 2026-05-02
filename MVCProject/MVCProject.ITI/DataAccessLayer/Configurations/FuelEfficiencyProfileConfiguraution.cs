using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVCProject.ITI.DataAccessLayer.Entities;
namespace MVCProject.ITI.DataAccessLayer.Configurations
{
    public class FuelEfficiencyProfileConfiguraution: IEntityTypeConfiguration<FuelEfficiencyProfile>
    {
        public void Configure(EntityTypeBuilder<FuelEfficiencyProfile> builder)
        {
            // Set default value for FuelEfficiencyProfile.Id to use sequential GUIDs for better performance
            builder.Property(f => f.Id)
                   .HasDefaultValueSql("NEWSEQUENTIALID()");


            // FuelEfficiencyProfileConfiguration.cs
            builder.HasOne(f => f.Vehicle)
                   .WithMany(v => v.FuelEfficiencyProfiles)
                   .HasForeignKey(f => f.VehicleId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(
                new FuelEfficiencyProfile
                {
                    Id = Guid.Parse("67890123-4567-8901-2345-678901234567"),
                    VehicleId = Guid.Parse("f9b5a7a9-2f22-4a7b-a454-077a28424294"),
                    ConsumptionRate = 5.0f,
                    Unit = "L/100km"
                }
            );
        }
    }
}
