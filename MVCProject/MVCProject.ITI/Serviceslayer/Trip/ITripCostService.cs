using MVCProject.ITI.Models.Trip;
using MVCProject.ITI.DataAccessLayer.Entities;
namespace MVCProject.ITI.Serviceslayer.Trip
{
    public interface ITripCostService
    {
        Task<TripCostResult> CalculateTripCostAsync(Guid vehicleId, string origin, string destination, bool isAcOn, DateTime tripDateTime);
    }
}
