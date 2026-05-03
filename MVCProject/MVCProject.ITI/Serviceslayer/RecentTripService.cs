using MVCProject.ITI.DataAccessLayer.Entities;
using MvcProject.iti.DataAccessLayer.Repository.GenericRepo;
using MVCProject.ITI.Models;
using AutoMapper;
using System.Collections.Generic;
using MVCProject.ITI.DataAccessLayer.Rpository.TripRepo;

namespace MVCProject.ITI.Serviceslayer
{
    public class RecentTripService : IRecentTripService
    {
        private readonly ITripRepo _tripRepo;
        private readonly IMapper _mapper;

        public RecentTripService(ITripRepo tripRepo,IMapper mapper)
        {
            _tripRepo = tripRepo;
            _mapper = mapper;
        }
        public async Task<IEnumerable<TripCardViewModel>> GetRecentTrips()
        {
            IEnumerable<Trip> RecentTrips =await _tripRepo.GetTripsWithVehicleAndCostResult();
            if (!RecentTrips.Any())
                return Enumerable.Empty<TripCardViewModel>();
             IEnumerable<TripCardViewModel> RecentTripsVM = _mapper.Map<IEnumerable<TripCardViewModel>>(RecentTrips);
            return RecentTripsVM;
        }
    }
}
