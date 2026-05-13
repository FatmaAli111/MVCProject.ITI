using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVCProject.ITI.ViewModels
{
    public class NewTripViewModel
    {
        [Required(ErrorMessage = "Starting location is required")]
        public string From { get; set; }

        [Required(ErrorMessage = "Destination is required")]
        public string To { get; set; }

        public bool LeaveNow { get; set; } = true;
        public bool IsAcOn { get; set; }

        public DateTime? ScheduledTime { get; set; } = DateTime.Now;
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Please select a vehicle from your garage")]
        public Guid VehicleId { get; set; }
        public List<SelectListItem>? AvailableVehicles { get; set; }
    }
}
