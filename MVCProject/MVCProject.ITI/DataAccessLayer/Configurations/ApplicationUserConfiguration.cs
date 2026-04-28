using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVCProject.ITI.DataAccessLayer.Entities;

namespace MVCProject.ITI.DataAccessLayer.Configurations
{
    public class ApplicationUserConfiguration: IEntityTypeConfiguration<ApplicationUser>
    {
        public ApplicationUserConfiguration() { }
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            // Rename the table
            builder.ToTable("Users");

            // Set default value for ApplicationUser.Id to use sequential GUIDs for better performance
            builder.Property(u => u.Id)
                   .HasDefaultValueSql("NEWSEQUENTIALID()");

            // No "builder.HasMany" here! It's already handled in the Trip config
            // I handled the relationships in the child entities (the ones that have foreign keys)
            // (Vehicle and Trip) to avoid circular configuration issues.
        }

    }
}
