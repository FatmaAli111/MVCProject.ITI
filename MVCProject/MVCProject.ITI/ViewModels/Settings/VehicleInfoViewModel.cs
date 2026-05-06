using MVCProject.ITI.DataAccessLayer.Entities;
using System.ComponentModel.DataAnnotations;

namespace MVCProject.ITI.ViewModels.Settings
{
    public class VehicleInfoViewModel
    {
        public Guid VehicleId { get; set; }
        
        [Required]
        public string Make { get; set; } = string.Empty;
        
        [Required]
        public string Model { get; set; } = string.Empty;
        
        [Required]
        public int Year { get; set; }
        
        [Required]
        [Range(1, 20)]
        public int PassengerCapacity { get; set; }
        
        [Required]
        public FuelTypeEnum FuelType { get; set; }
        
        public float WltpMixed { get; set; }
        
        [StringLength(50)]
        public string CarNickname { get; set; } = string.Empty;
        
        public bool IsOverride { get; set; }
        
        public List<string> AvailableMakes { get; set; } = new();
        public List<string> AvailableModels { get; set; } = new();
        public List<int> AvailableYears { get; set; } = new();
    }
}
