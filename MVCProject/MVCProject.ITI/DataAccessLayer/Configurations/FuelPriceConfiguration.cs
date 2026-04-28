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

            builder.HasData(
                new FuelPrice
                {
                    Id = Guid.Parse("85623838-8a8b-4a5d-b088-25679eff9011"),
                    FuelType = FuelTypeEnum.Gasoline,
                    Region = "Egypt",
                    PricePerUnit = 13.50f,
                    Currency = "EGP",
                    RecordedDate = new DateTime(2024, 1, 1)
                },
                new FuelPrice
                {
                    Id = Guid.Parse("a1d82f7c-50bc-4340-a3fc-211c4794e771"),
                    FuelType = FuelTypeEnum.Diesel,
                    Region = "Egypt",
                    PricePerUnit = 10.00f,
                    Currency = "EGP",
                    RecordedDate = new DateTime(2024, 1, 1)
                }
            );
        }
    }
}
