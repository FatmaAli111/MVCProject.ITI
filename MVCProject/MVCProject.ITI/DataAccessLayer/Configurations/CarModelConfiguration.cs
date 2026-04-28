using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVCProject.ITI.DataAccessLayer.Entities;

namespace MVCProject.ITI.DataAccessLayer.Configurations
{
    public class CarModelConfiguration: IEntityTypeConfiguration<CarModel>
    {
        public void Configure(EntityTypeBuilder<CarModel> builder)
        {
            // Set default value for CarModel.Id to use sequential GUIDs for better performance
            builder.Property(cm => cm.Id)
                   .HasDefaultValueSql("NEWSEQUENTIALID()");

            builder.HasData(
                new CarModel
                {
                    Id = Guid.Parse("13735163-952a-466d-8e7c-87d3dfa7263b"),
                    Make = "Toyota",
                    Model = "Corolla",
                    Year = 2023,
                    FuelType = FuelTypeEnum.Gasoline,
                    WltpCity = 5.5f,
                    WltpHighway = 4.2f,
                    WltpMixed = 4.9f
                },
                new CarModel
                {
                    Id = Guid.Parse("279318b7-6e4e-4f33-87b6-e2a7e7ed27d6"),
                    Make = "Honda",
                    Model = "Civic",
                    Year = 2023,
                    FuelType = FuelTypeEnum.Gasoline,
                    WltpCity = 6.2f,
                    WltpHighway = 4.8f,
                    WltpMixed = 5.5f
                }
            );
        }
    }
}
