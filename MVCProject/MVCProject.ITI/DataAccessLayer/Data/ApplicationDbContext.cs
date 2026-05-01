using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
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

        builder.Entity<IdentityRole<Guid>>().ToTable("Roles");
        builder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles");
        builder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims");
        builder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins");
        builder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims");
        builder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens");

        // Seed Roles
        var adminRoleId = Guid.Parse("d6e87f16-7788-4f10-9c1c-0c3f09f023ea");
        var userRoleId = Guid.Parse("f6e87f16-7788-4f10-9c1c-0c3f09f023eb");

        builder.Entity<IdentityRole<Guid>>().HasData(
            new IdentityRole<Guid> { Id = adminRoleId, Name = "Admin", NormalizedName = "ADMIN" },
            new IdentityRole<Guid> { Id = userRoleId, Name = "User", NormalizedName = "USER" }
        );

        // Seed UserRole (Assign Admin to Admin User)
        builder.Entity<IdentityUserRole<Guid>>().HasData(
            new IdentityUserRole<Guid> { UserId = Guid.Parse("46686121-d1c1-4796-993d-82d2a45a6660"), RoleId = adminRoleId }
        );

    }

}

