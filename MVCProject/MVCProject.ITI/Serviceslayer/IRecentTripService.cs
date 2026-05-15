using MVCProject.ITI.DataAccessLayer.Entities;
using MVCProject.ITI.Extensions;
using MVCProject.ITI.Models;
using MVCProject.ITI.ViewModels;

namespace MVCProject.ITI.Serviceslayer
{
    public interface IRecentTripService
    {
        Task<IEnumerable<TripCardViewModel>> GetRecentTrips(Guid id);
        Task<IEnumerable<TripCardViewModel>> GetAllTrips(Guid userId);
        Task<PaginatedResult<TripCardViewModel>> GetTripsPagedAsync(Guid userId, int page, int pageSize = 10);
        Task AddTrip(NewTripViewModel newTripViewModel);
    }
}
