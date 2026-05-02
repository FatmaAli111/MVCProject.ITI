using MVCProject.ITI.DataAccessLayer.Entities;

namespace MVCProject.ITI.ViewModels
{
    public class AddVechileViewModel
    {
       
        public string NickName { get; set; } = string.Empty;
        public string Make { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        public string ColorHex { get; set; } = "#800000";
        public float WltpMixed { get; set; }
        public float TankCapacity { get; set; }
        public Guid VehicleId { get; set; }
        public Guid CarModelId { get; set; }
        public float BatteryCapacity { get; set; }
        public FuelTypeEnum FuelType { get; set; }

    }
}
