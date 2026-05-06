using System.ComponentModel.DataAnnotations;

namespace MVCProject.ITI.ViewModels.Settings
{
    public class ProfileViewModel
    {
        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Phone]
        [StringLength(20)]
        public string? PhoneNumber { get; set; }
    }
}
