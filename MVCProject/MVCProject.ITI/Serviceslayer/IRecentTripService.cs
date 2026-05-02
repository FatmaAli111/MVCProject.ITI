using MVCProject.ITI.DataAccessLayer.Entities;
using MVCProject.ITI.Models;

namespace MVCProject.ITI.Serviceslayer
{
    public interface IRecentTripService
    {
        Task<IEnumerable<TripCardViewModel>> GetRecentTrips();
    }
}
