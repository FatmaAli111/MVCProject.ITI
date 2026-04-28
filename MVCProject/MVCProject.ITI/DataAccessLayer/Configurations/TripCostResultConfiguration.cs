using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVCProject.ITI.DataAccessLayer.Entities;

namespace MVCProject.ITI.DataAccessLayer.Configurations
{
    public class TripCostResultConfiguration: IEntityTypeConfiguration<TripCostResult>
    {


        public void Configure(EntityTypeBuilder<TripCostResult> builder)
        {
            // Set default value for TripCostResult.Id to use sequential GUIDs for better performance
            builder.Property(t => t.Id)
                   .HasDefaultValueSql("NEWSEQUENTIALID()");

            builder.Property(t => t.CalculatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");

            // Configure the relationship with FuelPrice
            builder.HasOne(t => t.FuelPrice)
                   .WithMany(f => f.TripCostResults)
                   .HasForeignKey(t => t.FuelPriceId)
                   .OnDelete(DeleteBehavior.Restrict);
          
            // Configure the relationship with Trip
            builder.HasOne(t => t.Trip)
               .WithOne(t => t.TripCostResult)
               .HasForeignKey<TripCostResult>(t => t.TripId)
               .OnDelete(DeleteBehavior.Cascade); // deleting a trip deletes its result


        }
    }
}
