using MVCProject.ITI.DataAccessLayer.Entities;
using System.ComponentModel.DataAnnotations;

namespace MVCProject.ITI.ViewModels
{
    public class AddVechileViewModel
    {
        [Required(ErrorMessage = "Nickname is required")]
        public string NickName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Brand is required")]
        public string Make { get; set; } = string.Empty;
        [Required(ErrorMessage = "Model is required")]
        public string Model { get; set; } = string.Empty;
        [Required(ErrorMessage = "Year is required")]
        [Range(1900, 2100, ErrorMessage = "Enter a valid year")]
        public int Year { get; set; }
        public string ColorHex { get; set; } = "#800000";
        [Required(ErrorMessage = "WLTP mixed is required")]
        [Range(1, 50, ErrorMessage = "Enter valid WLTP mixed value")]
        public float WltpMixed { get; set; }
        [Range(0, 200, ErrorMessage = "Enter valid tank capacity")]
        public float? TankCapacity { get; set; }
        public Guid VehicleId { get; set; }
        public Guid CarModelId { get; set; }
        [Range(0, 200, ErrorMessage = "Enter valid battery capacity")]
        public float? BatteryCapacity { get; set; }
        public FuelTypeEnum FuelType { get; set; }
        public Guid UserId { get; set; } = Guid.Empty;


    }
}
