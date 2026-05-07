using MVCProject.ITI.DataAccessLayer.Entities;
using MVCProject.ITI.Models;
using MVCProject.ITI.ViewModels;

namespace MVCProject.ITI.Serviceslayer
{
    public interface IRecentTripService
    {
        Task<IEnumerable<TripCardViewModel>> GetRecentTrips(Guid id);
        Task<IEnumerable<TripCardViewModel>> GetAllTrips(Guid userId);
        Task AddTrip(NewTripViewModel newTripViewModel);
    }
}
