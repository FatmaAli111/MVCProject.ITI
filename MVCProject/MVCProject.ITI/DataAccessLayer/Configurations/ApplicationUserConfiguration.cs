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

            var adminUser = new ApplicationUser
            {
                Id = Guid.Parse("46686121-d1c1-4796-993d-82d2a45a6660"),
                UserName = "admin@trips.com",
                NormalizedUserName = "ADMIN@TRIPS.COM",
                Email = "admin@trips.com",
                NormalizedEmail = "ADMIN@TRIPS.COM",
                EmailConfirmed = true,
                SecurityStamp = "f0883b27-c1d1-4e63-9993-82d2a45a6660", // Hardcoded Guid instead of Guid.NewGuid()
                PasswordHash = "AQAAAAIAAYagAAAAEJ6Y8+qHjG/f/8+7G1Ww0W5f6+9Q5f6+9Q5f6+9Q5f6+9Q==" // Default Password: Password123!
            };

            builder.HasData(adminUser);

            // No "builder.HasMany" here! It's already handled in the Trip config
            // I handled the relationships in the child entities (the ones that have foreign keys)
            // (Vehicle and Trip) to avoid circular configuration issues.
        }

    }
}
