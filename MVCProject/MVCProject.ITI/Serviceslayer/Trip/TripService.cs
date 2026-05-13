using Microsoft.EntityFrameworkCore;
using MVCProject.ITI.DataAccessLayer.Data;
using MVCProject.ITI.DataAccessLayer.Entities;
using MVCProject.ITI.Models;
using MVCProject.ITI.ViewModels;

namespace MVCProject.ITI.Serviceslayer.Trip
{
    public class TripService : ITripService
    {
        private readonly ApplicationDbContext _context;
        private readonly IRouteService _routeService;

        public TripService(ApplicationDbContext context, IRouteService routeService)
        {
            _context = context;
            _routeService = routeService;
        }

        public async Task<CompletionTripViewModel?> GetCompletionTripAsync(Guid tripId)
        {
            var trip = await _context.Trips
                .Include(t => t.Vehicle)
                .Include(t => t.TripCostResult)
                .Include(t => t.TripPassengers)
                .FirstOrDefaultAsync(t => t.Id == tripId);

            if (trip == null)
                return null;

            var routes = await _routeService.GetRoutesAsync(trip.OriginName, trip.DestinationName);
            var bestRoute = routes.FirstOrDefault();

            var fuelCost = (trip.TripCostResult?.FuelConsumed ?? 0) * 18.5;
            var maintenanceCost = trip.DistanceKm * 0.75;

            return new CompletionTripViewModel
            {
                TripId = trip.Id,
                FromName = trip.OriginName,
                ToName = trip.DestinationName,
                DistanceKm = trip.DistanceKm,
                DurationMinutes = trip.DurationMinutes,
                TripDate = trip.TripDate,

                FromLat = bestRoute?.StartLat ?? 0,
                FromLng = bestRoute?.StartLng ?? 0,
                ToLat = bestRoute?.EndLat ?? 0,
                ToLng = bestRoute?.EndLng ?? 0,

                CarName = trip.Vehicle?.NickName ?? "Vehicle",

                TotalCost = trip.TripCostResult?.TotalCost ?? 0,
                FuelConsumed = trip.TripCostResult?.FuelConsumed ?? 0,
                TrafficCondition = trip.TripCostResult?.TrafficCondition ?? "Normal",
                WeatherCondition = trip.TripCostResult?.WeatherCondition ?? "Clear",

                FuelCost = fuelCost,
                MaintenanceCost = maintenanceCost,

                Passengers = trip.TripPassengers
                    .OrderBy(p => p.Name)
                    .Select(p => new PassengerSplitVM
                    {
                        Id = p.Id,
                        Name = p.Name,
                        ShareAmount = p.ShareAmount,
                        SharePercentage = p.SharePercentage
                    })
                    .ToList()
            };
        }

      

        public async Task<bool> SaveTripSplitAsync(SplitTripViewModel vm, Guid userId)
        {
            if (vm.Passengers == null || !vm.Passengers.Any())
                return false;

            var trip = await _context.Trips
                .FirstOrDefaultAsync(t => t.Id == vm.TripId);

            if (trip == null || trip.UserId != userId)
                return false;

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var existingPassengers = await _context.TripPassengers
                    .Where(p => p.TripId == vm.TripId)
                    .ToListAsync();

                _context.TripPassengers.RemoveRange(existingPassengers);

                foreach (var p in vm.Passengers)
                {
                    _context.TripPassengers.Add(new TripPassenger
                    {
                        Id = Guid.NewGuid(),
                        TripId = vm.TripId,
                        Name = p.Name,
                        ShareAmount = p.ShareAmount,
                        SharePercentage = p.SharePercentage
                    });
                }

                trip.PassengerCount = vm.Passengers.Count + 1;
                _context.Trips.Update(trip);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
