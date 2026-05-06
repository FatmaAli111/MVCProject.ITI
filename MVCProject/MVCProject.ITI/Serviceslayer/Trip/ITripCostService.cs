using MVCProject.ITI.Models.Trip;

namespace MVCProject.ITI.Serviceslayer.Trip
{
    public interface ITripCostService
    {
        Task<TripCostCalculation> CalculateAsync(
            string origin,
            string destination,
            float distanceKm,
            float fuelPricePerLiter,
            float fuelEfficiencyL100km,
            int passengerCount,
            bool isAcOn);
    }
}
