namespace MVCProject.ITI.DataAccessLayer.Entities
{
    public enum FuelTypeEnum
    {
        Gasoline,
        Diesel,
        Electric,
        Hybrid
    }
    public class CarModel
    {
        // This table is for storing the car model information from the official manufacturer itself
        public Guid Id { get; set; }
        public string Make { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        public float WltpCity { get; set; }
        public float WltpHighway { get; set; }
        public float WltpMixed { get; set; }
        
        // Relationships ***********
        public FuelTypeEnum FuelType { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; } = [];

    }
}
