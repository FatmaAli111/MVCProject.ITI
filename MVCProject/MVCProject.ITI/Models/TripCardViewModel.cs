
using System.ComponentModel.DataAnnotations;

namespace MVCProject.ITI.Models
{
    public class TripCardViewModel
    {
        public string DestinationName { get; set; } = string.Empty;
        public string OriginName { get; set; } = string.Empty;

        public float DistanceKm { get; set; }
        public int DurationMinutes { get; set; }
        [DataType(DataType.Date)]
        public DateTime TripDate { get; set; }
        public string VehicleName { get; set; } 
        public float TripTotalCost { get; set; } 

    }
}
