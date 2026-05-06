using MvcProject.iti.DataAccessLayer.Repository.GenericRepo;
using MVCProject.ITI.DataAccessLayer.Entities;

namespace MVCProject.ITI.DataAccessLayer.Rpository.TripRepo
{
    public interface ITripRepo:IGenericRepository<Trip>
    {
        Task<IEnumerable<Trip>> GetTripsWithVehicleAndCostResult(Guid id);
    }
}
