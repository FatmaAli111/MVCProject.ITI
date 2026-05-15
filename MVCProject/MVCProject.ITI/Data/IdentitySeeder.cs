using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVCProject.ITI.Constants;
using MVCProject.ITI.DataAccessLayer.Entities;

namespace MVCProject.ITI.Data;

public static class IdentitySeeder
{
    public static async Task SeedAsync(IServiceProvider services, IConfiguration configuration)
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

        if (!await roleManager.RoleExistsAsync(RoleNames.Admin))
            await roleManager.CreateAsync(new IdentityRole<Guid>(RoleNames.Admin));

        var seedEmail = configuration["Admin:SeedEmail"];
        var seedPassword = configuration["Admin:SeedPassword"];
        var seedFullName = configuration["Admin:SeedFullName"] ?? "System Administrator";

        if (string.IsNullOrWhiteSpace(seedEmail) || string.IsNullOrWhiteSpace(seedPassword))
            return;

        var adminUser = await userManager.FindByEmailAsync(seedEmail);
        if (adminUser is null)
        {
            adminUser = new ApplicationUser
            {
                UserName = seedEmail,
                Email = seedEmail,
                FullName = seedFullName,
                EmailConfirmed = true
            };

            var createResult = await userManager.CreateAsync(adminUser, seedPassword);
            if (!createResult.Succeeded)
                return;
        }
        else if (!adminUser.EmailConfirmed)
        {
            adminUser.EmailConfirmed = true;
            await userManager.UpdateAsync(adminUser);
        }

        if (!await userManager.IsInRoleAsync(adminUser, RoleNames.Admin))
            await userManager.AddToRoleAsync(adminUser, RoleNames.Admin);
    }
}
