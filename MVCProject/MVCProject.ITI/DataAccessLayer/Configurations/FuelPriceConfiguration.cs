using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVCProject.ITI.DataAccessLayer.Entities;

namespace MVCProject.ITI.DataAccessLayer.Configurations
{
    public class FuelPriceConfiguration: IEntityTypeConfiguration<FuelPrice>
    {
        public void Configure(EntityTypeBuilder<FuelPrice> builder)
        {
            // Set default value for FuelPrice.Id to use sequential GUIDs for better performance
            builder.Property(f => f.Id)
                   .HasDefaultValueSql("NEWSEQUENTIALID()");
        }
    }
}
