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
        }
    }
}
