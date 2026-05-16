using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCProject.ITI.Areas.Admin.ViewModels;
using MVCProject.ITI.Constants;
using MVCProject.ITI.DataAccessLayer.Entities;
using MVCProject.ITI.Extensions;

namespace MVCProject.ITI.Areas.Admin.Controllers;

public class UsersController : AdminControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;
    private readonly IConfiguration _configuration;

    public UsersController(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole<Guid>> roleManager,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }

    public async Task<IActionResult> Index(int page = 1, string? search = null)
    {
        var query = _userManager.Users.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim().ToLower();
            query = query.Where(u =>
                (u.Email != null && u.Email.ToLower().Contains(term)) ||
                u.FullName.ToLower().Contains(term));
        }

        query = query.OrderByDescending(u => u.EmailConfirmed).ThenBy(u => u.Email);

        var paged = await query
            .Select(u => new AdminUserListItemViewModel
            {
                Id = u.Id,
                Email = u.Email ?? "",
                FullName = u.FullName,
                EmailConfirmed = u.EmailConfirmed,
                LockoutEnabled = u.LockoutEnabled,
                LockoutEnd = u.LockoutEnd
            })
            .ToPaginatedListAsync(page, 12);

        foreach (var item in paged.Data)
        {
            var user = await _userManager.FindByIdAsync(item.Id.ToString());
            if (user is not null)
                item.Roles = await _userManager.GetRolesAsync(user);
        }

        ViewBag.Search = search;
        return View(paged);
    }

    public async Task<IActionResult> Details(Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user is null)
            return NotFound();

        var roles = await _userManager.GetRolesAsync(user);
        var vm = new AdminUserListItemViewModel
        {
            Id = user.Id,
            Email = user.Email ?? "",
            FullName = user.FullName,
            EmailConfirmed = user.EmailConfirmed,
            LockoutEnabled = user.LockoutEnabled,
            LockoutEnd = user.LockoutEnd,
            Roles = roles
        };

        return View(vm);
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user is null)
            return NotFound();

        var roles = await _userManager.GetRolesAsync(user);
        var currentUser = await _userManager.GetUserAsync(User);
        return View(new AdminUserEditViewModel
        {
            Id = user.Id,
            Email = user.Email ?? "",
            FullName = user.FullName,
            EmailConfirmed = user.EmailConfirmed,
            IsAdmin = roles.Contains(RoleNames.Admin),
            IsLockedOut = user.LockoutEnd.HasValue && user.LockoutEnd > DateTimeOffset.UtcNow,
            CanManageAdmins = AdminAuthorization.IsSuperAdmin(currentUser, _configuration)
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(AdminUserEditViewModel model)
    {
        var user = await _userManager.FindByIdAsync(model.Id.ToString());
        if (user is null)
            return NotFound();

        if (!ModelState.IsValid)
            return View(model);

        user.FullName = model.FullName.Trim();
        user.EmailConfirmed = model.EmailConfirmed;

        if (model.IsLockedOut)
            await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow.AddYears(100));
        else
            await _userManager.SetLockoutEndDateAsync(user, null);

        var updateResult = await _userManager.UpdateAsync(user);
        if (!updateResult.Succeeded)
        {
            foreach (var error in updateResult.Errors)
                ModelState.AddModelError(string.Empty, error.Description);
            return View(model);
        }

        var currentUser = await _userManager.GetUserAsync(User);
        if (AdminAuthorization.IsSuperAdmin(currentUser, _configuration))
        {
            var isAdmin = await _userManager.IsInRoleAsync(user, RoleNames.Admin);
            if (model.IsAdmin && !isAdmin)
                await _userManager.AddToRoleAsync(user, RoleNames.Admin);
            else if (!model.IsAdmin && isAdmin)
            {
                if (AdminAuthorization.IsSuperAdmin(user, _configuration))
                {
                    SetError("The system administrator account cannot be removed from the Admin role.");
                    return RedirectToAction(nameof(Details), new { id = user.Id });
                }
                await _userManager.RemoveFromRoleAsync(user, RoleNames.Admin);
            }
        }

        SetSuccess("User updated successfully.");
        return RedirectToAction(nameof(Details), new { id = user.Id });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user is null)
            return NotFound();

        var currentUserId = _userManager.GetUserId(User);
        if (currentUserId == user.Id.ToString())
        {
            SetError("You cannot delete your own account while signed in.");
            return RedirectToAction(nameof(Index));
        }

        var result = await _userManager.DeleteAsync(user);
        if (result.Succeeded)
            SetSuccess("User deleted.");
        else
            SetError(string.Join(" ", result.Errors.Select(e => e.Description)));

        return RedirectToAction(nameof(Index));
    }
}
