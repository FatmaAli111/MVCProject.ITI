using MVCProject.ITI.ViewModels;

namespace MVCProject.ITI.Serviceslayer.Trip
{
    public interface ITripService
    {
        Task<CompletionTripViewModel?> GetCompletionTripAsync(Guid tripId);
        Task<bool> SaveTripSplitAsync(SplitTripViewModel vm, Guid userId);
    }
}
