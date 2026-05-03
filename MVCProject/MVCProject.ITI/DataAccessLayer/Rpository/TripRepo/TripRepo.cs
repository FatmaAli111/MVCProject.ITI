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

        public async Task<IEnumerable<Trip>> GetTripsWithVehicleAndCostResult()
        {
            var TripsWithVehicleAndCostResult=  _dbContext.Trips.Include(v => v.Vehicle)
                .Include(c => c.TripCostResult).OrderByDescending(d => d.TripDate)
                .Take(3).ToList();
            return TripsWithVehicleAndCostResult;
        }
    }
}
