using MVCProject.ITI.DataAccessLayer.Entities;
using MvcProject.iti.DataAccessLayer.Repository.GenericRepo;
using MVCProject.ITI.Models;
using AutoMapper;
using System.Collections.Generic;
using MVCProject.ITI.DataAccessLayer.Rpository.TripRepo;
using MVCProject.ITI.ViewModels;


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

        public async Task AddTrip(NewTripViewModel newTripViewModel)
        {
            if (newTripViewModel.LeaveNow == true)
                newTripViewModel.ScheduledTime = DateTime.Now;
            var newTrip = _mapper.Map<ITI.DataAccessLayer.Entities.Trip>(newTripViewModel);
            try
            {
                _tripRepo.Add(newTrip);
                _tripRepo.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<TripCardViewModel>> GetAllTrips(Guid userId)
        {
            IEnumerable<MVCProject.ITI.DataAccessLayer.Entities.Trip> AllTrips =
                _tripRepo.GetTableNoTracking().Where(t=>t.UserId== userId);


            if (!AllTrips.Any())
                return Enumerable.Empty<TripCardViewModel>();
            IEnumerable<TripCardViewModel> recentTripsVM = _mapper.Map<IEnumerable<TripCardViewModel>>(AllTrips);
            return recentTripsVM;
        }

        public async Task<IEnumerable<TripCardViewModel>> GetRecentTrips(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Invalid user ID");

            IEnumerable<MVCProject.ITI.DataAccessLayer.Entities.Trip> recentTrips = await _tripRepo.GetTripsWithVehicleAndCostResult(id);
          
            if (!recentTrips.Any())
                return Enumerable.Empty<TripCardViewModel>();
             IEnumerable<TripCardViewModel> recentTripsVM = _mapper.Map<IEnumerable<TripCardViewModel>>(recentTrips);
            return recentTripsVM;
        }
        

      
    }
}
