using System.ComponentModel.DataAnnotations;

namespace MVCProject.ITI.ViewModels
{
    public class NewTripViewModel
    {
        [Required(ErrorMessage = "Starting location is required")]
        public string From { get; set; }

        [Required(ErrorMessage = "Destination is required")]
        public string To { get; set; }

        public bool LeaveNow { get; set; } = true;

        public DateTime? ScheduledTime { get; set; }
    }
}
