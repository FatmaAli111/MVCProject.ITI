using System;
using System.Collections.Generic;

namespace MVCProject.ITI.ViewModels
{
    public class CompletionTripViewModel
{
    public Guid TripId { get; set; }
    public string FromName { get; set; }
    public string ToName { get; set; }
    public double DistanceKm { get; set; }
    public int DurationMinutes { get; set; }
    public DateTime TripDate { get; set; }
    public double FromLat { get; set; }
    public double FromLng { get; set; }
    public double ToLat { get; set; }
    public double ToLng { get; set; }
    public string CarName { get; set; }
    public double TotalCost { get; set; }
    public double FuelConsumed { get; set; }
    public string WeatherCondition { get; set; }
    public string TrafficCondition { get; set; }
    public int PassengerCount { get; set; }
    public bool IsAcOn { get; set; }

    public string DurationString => $"{DurationMinutes / 60}h {DurationMinutes % 60}m";
    public string CarEfficiency { get; set; } = "Standard Efficiency";
    public string CO2Kg => (FuelConsumed * 2.31).ToString("F1");
    public double FuelCost { get; set; }
    public double FuelPercentage => TotalCost > 0 ? (FuelCost / TotalCost) * 100 : 0;
    public double TollCost { get; set; }
    public double MaintenanceCost { get; set; }
    public double UberCost { get; set; }
    public double TaxiCost { get; set; }
    public double WeatherMultiplier { get; set; } = 1.0;
    public double TrafficMultiplier { get; set; } = 1.0;
        public List<PassengerSplitVM> Passengers { get; set; } = new();
    }
}