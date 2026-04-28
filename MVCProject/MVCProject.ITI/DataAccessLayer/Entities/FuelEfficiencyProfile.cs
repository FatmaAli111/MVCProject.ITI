using System.ComponentModel.DataAnnotations;

namespace MVCProject.ITI.DataAccessLayer.Entities
{
    public enum DrivingCondationEnum
    {
        City,
        Highway,
        Mixed
    }
    public class FuelEfficiencyProfile
    {
        public Guid Id { get; set; }
        public Guid VehicleId { get; set; }
        public DrivingCondationEnum DrivingCondation { get; set; } = DrivingCondationEnum.City;
        public float ConsumptionRate { get; set; }
        public string Unit { get; set; } = "L/100km";

        // Relationships ***********

        public Vehicle Vehicle { get; set; }

    }
}
