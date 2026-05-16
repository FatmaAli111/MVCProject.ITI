using Microsoft.AspNetCore.Mvc;
using MVCProject.ITI.Serviceslayer.Admin;

namespace MVCProject.ITI.Areas.Admin.Controllers;

public class DashboardController : AdminControllerBase
{
    private readonly IAdminDashboardService _dashboardService;

    public DashboardController(IAdminDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var model = await _dashboardService.GetDashboardAsync(cancellationToken);
        return View(model);
    }
}
