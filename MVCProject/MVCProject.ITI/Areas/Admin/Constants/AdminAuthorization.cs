using Microsoft.AspNetCore.Identity;
using MVCProject.ITI.DataAccessLayer.Entities;

namespace MVCProject.ITI.Constants;

public static class AdminAuthorization
{
    public static bool IsSuperAdmin(ApplicationUser? user, IConfiguration configuration)
    {
        if (user is null || string.IsNullOrWhiteSpace(user.Email))
            return false;

        var seedEmail = configuration["Admin:SeedEmail"];
        if (string.IsNullOrWhiteSpace(seedEmail))
            return false;

        return user.Email.Equals(seedEmail, StringComparison.OrdinalIgnoreCase);
    }

    public static async Task<bool> IsSuperAdminAsync(
        UserManager<ApplicationUser> userManager,
        IConfiguration configuration,
        System.Security.Claims.ClaimsPrincipal principal)
    {
        var user = await userManager.GetUserAsync(principal);
        return IsSuperAdmin(user, configuration);
    }
}
