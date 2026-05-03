using Microsoft.EntityFrameworkCore;
using MvcProject.iti.DataAccessLayer.Repository.GenericRepo;
using MVCProject.ITI.DataAccessLayer.Data;
using MVCProject.ITI.DataAccessLayer.Entities;

namespace MVCProject.ITI.DataAccessLayer.Rpository.TripRepo
{
    public class TripRepo : GenericRepository<Trip>, ITripRepo
    {
        public TripRepo(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Trip>> GetTripsWithVehicleAndCostResult(Guid id)
        {
            var TripsWithVehicleAndCostResult=await  _dbContext.Trips.Where(t => t.UserId == id).Include(t => t.Vehicle)
                                                .Include(t => t.TripCostResult)
                                                .OrderByDescending(t => t.TripDate).Take(3).ToListAsync();
            if (TripsWithVehicleAndCostResult is null)
                return Enumerable.Empty<Trip>();
            return TripsWithVehicleAndCostResult;
        }
    }
}
