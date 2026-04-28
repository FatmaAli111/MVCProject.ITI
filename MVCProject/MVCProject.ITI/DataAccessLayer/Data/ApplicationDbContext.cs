using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using MVCProject.ITI.DataAccessLayer.Entities;
using System.Security.Principal;

namespace MVCProject.ITI.DataAccessLayer.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Trip> Trips { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<CarModel> Cars { get; set; }
    public DbSet<FuelPrice> FuelPrices { get; set; }
    public DbSet<TripCostResult> TripCostResults { get; set; }
    public DbSet<FuelEfficiencyProfile> FuelEfficiencyProfiles { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        // Rename the remaining Identity tables manually in the db
        builder.Entity<IdentityRole<Guid>>().ToTable("Roles");
            builder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles");
            builder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims");
            builder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins");
            builder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens");
        
    }

}

