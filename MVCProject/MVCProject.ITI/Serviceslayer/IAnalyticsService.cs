using MVCProject.ITI.ViewModels;

namespace MVCProject.ITI.Serviceslayer
{
    public interface IAnalyticsService
    {
        Task<AnalyticsViewModel> calcAnalytics(Guid userId);
    }
}
