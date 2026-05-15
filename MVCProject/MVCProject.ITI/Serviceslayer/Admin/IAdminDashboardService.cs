using MVCProject.ITI.Areas.Admin.ViewModels;

namespace MVCProject.ITI.Serviceslayer.Admin;

public interface IAdminDashboardService
{
    Task<AdminDashboardViewModel> GetDashboardAsync(CancellationToken cancellationToken = default);
}
