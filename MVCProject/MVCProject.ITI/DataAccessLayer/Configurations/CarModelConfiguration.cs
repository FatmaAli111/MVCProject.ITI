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
        }
    }
}
