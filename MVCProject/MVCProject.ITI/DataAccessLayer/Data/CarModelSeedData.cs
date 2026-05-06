using Microsoft.EntityFrameworkCore;
using MVCProject.ITI.DataAccessLayer.Entities;

namespace MVCProject.ITI.DataAccessLayer.Data
{
    public static class CarModelSeedData
    {
        public static void Seed(ModelBuilder builder)
        {
            builder.Entity<CarModel>().HasData(
                // Toyota (excluding duplicates from migration)
                new CarModel
                {
                    Id = Guid.Parse("379318b7-6e4e-4f33-87b6-e2a7e7ed27d7"),
                    Make = "Toyota",
                    Model = "Camry",
                    Year = 2023,
                    WltpMixed = 5.8f,
                    TankCapacity = 55f,
                    FuelType = FuelTypeEnum.Hybrid
                },
                new CarModel
                {
                    Id = Guid.Parse("479318b7-6e4e-4f33-87b6-e2a7e7ed27d8"),
                    Make = "Toyota",
                    Model = "RAV4",
                    Year = 2023,
                    WltpMixed = 6.2f,
                    TankCapacity = 55f,
                    FuelType = FuelTypeEnum.Hybrid
                },
                new CarModel
                {
                    Id = Guid.Parse("579318b7-6e4e-4f33-87b6-e2a7e7ed27d9"),
                    Make = "Toyota",
                    Model = "Prius",
                    Year = 2023,
                    WltpMixed = 4.0f,
                    TankCapacity = 43f,
                    FuelType = FuelTypeEnum.Hybrid
                },
                new CarModel
                {
                    Id = Guid.Parse("679318b7-6e4e-4f33-87b6-e2a7e7ed27da"),
                    Make = "Toyota",
                    Model = "Corolla",
                    Year = 2022,
                    WltpMixed = 5.1f,
                    TankCapacity = 50f,
                    FuelType = FuelTypeEnum.Gasoline
                },
                new CarModel
                {
                    Id = Guid.Parse("779318b7-6e4e-4f33-87b6-e2a7e7ed27db"),
                    Make = "Toyota",
                    Model = "Corolla",
                    Year = 2024,
                    WltpMixed = 4.8f,
                    TankCapacity = 50f,
                    FuelType = FuelTypeEnum.Gasoline
                },
                // Honda
                new CarModel
                {
                    Id = Guid.Parse("879318b7-6e4e-4f33-87b6-e2a7e7ed27dc"),
                    Make = "Honda",
                    Model = "Civic",
                    Year = 2022,
                    WltpMixed = 5.7f,
                    TankCapacity = 47f,
                    FuelType = FuelTypeEnum.Gasoline
                },
                new CarModel
                {
                    Id = Guid.Parse("979318b7-6e4e-4f33-87b6-e2a7e7ed27dd"),
                    Make = "Honda",
                    Model = "Civic",
                    Year = 2024,
                    WltpMixed = 5.3f,
                    TankCapacity = 47f,
                    FuelType = FuelTypeEnum.Gasoline
                },
                new CarModel
                {
                    Id = Guid.Parse("a79318b7-6e4e-4f33-87b6-e2a7e7ed27de"),
                    Make = "Honda",
                    Model = "Accord",
                    Year = 2023,
                    WltpMixed = 6.0f,
                    TankCapacity = 48f,
                    FuelType = FuelTypeEnum.Hybrid
                },
                new CarModel
                {
                    Id = Guid.Parse("b79318b7-6e4e-4f33-87b6-e2a7e7ed27df"),
                    Make = "Honda",
                    Model = "CR-V",
                    Year = 2023,
                    WltpMixed = 6.8f,
                    TankCapacity = 53f,
                    FuelType = FuelTypeEnum.Hybrid
                },
                // BMW
                new CarModel
                {
                    Id = Guid.Parse("c79318b7-6e4e-4f33-87b6-e2a7e7ed27e0"),
                    Make = "BMW",
                    Model = "3 Series",
                    Year = 2023,
                    WltpMixed = 5.9f,
                    TankCapacity = 59f,
                    FuelType = FuelTypeEnum.Diesel
                },
                new CarModel
                {
                    Id = Guid.Parse("d79318b7-6e4e-4f33-87b6-e2a7e7ed27e1"),
                    Make = "BMW",
                    Model = "3 Series",
                    Year = 2023,
                    WltpMixed = 6.5f,
                    TankCapacity = 59f,
                    FuelType = FuelTypeEnum.Gasoline
                },
                new CarModel
                {
                    Id = Guid.Parse("e79318b7-6e4e-4f33-87b6-e2a7e7ed27e2"),
                    Make = "BMW",
                    Model = "i4",
                    Year = 2023,
                    WltpMixed = 16.5f,
                    BatteryCapacity = 84f,
                    FuelType = FuelTypeEnum.Electric
                },
                // Mercedes
                new CarModel
                {
                    Id = Guid.Parse("f79318b7-6e4e-4f33-87b6-e2a7e7ed27e3"),
                    Make = "Mercedes",
                    Model = "C-Class",
                    Year = 2023,
                    WltpMixed = 6.2f,
                    TankCapacity = 66f,
                    FuelType = FuelTypeEnum.Diesel
                },
                new CarModel
                {
                    Id = Guid.Parse("079318b7-6e4e-4f33-87b6-e2a7e7ed27e4"),
                    Make = "Mercedes",
                    Model = "EQE",
                    Year = 2023,
                    WltpMixed = 18.0f,
                    BatteryCapacity = 90f,
                    FuelType = FuelTypeEnum.Electric
                },
                // Volkswagen
                new CarModel
                {
                    Id = Guid.Parse("179318b7-6e4e-4f33-87b6-e2a7e7ed27e5"),
                    Make = "Volkswagen",
                    Model = "Golf",
                    Year = 2023,
                    WltpMixed = 5.4f,
                    TankCapacity = 50f,
                    FuelType = FuelTypeEnum.Gasoline
                },
                new CarModel
                {
                    Id = Guid.Parse("279318b7-6e4e-4f33-87b6-e2a7e7ed27e6"),
                    Make = "Volkswagen",
                    Model = "ID.4",
                    Year = 2023,
                    WltpMixed = 17.5f,
                    BatteryCapacity = 82f,
                    FuelType = FuelTypeEnum.Electric
                },
                new CarModel
                {
                    Id = Guid.Parse("379318b7-6e4e-4f33-87b6-e2a7e7ed27e7"),
                    Make = "Volkswagen",
                    Model = "Passat",
                    Year = 2023,
                    WltpMixed = 5.2f,
                    TankCapacity = 66f,
                    FuelType = FuelTypeEnum.Diesel
                },
                // Ford
                new CarModel
                {
                    Id = Guid.Parse("479318b7-6e4e-4f33-87b6-e2a7e7ed27e8"),
                    Make = "Ford",
                    Model = "Focus",
                    Year = 2023,
                    WltpMixed = 5.6f,
                    TankCapacity = 52f,
                    FuelType = FuelTypeEnum.Gasoline
                },
                new CarModel
                {
                    Id = Guid.Parse("579318b7-6e4e-4f33-87b6-e2a7e7ed27e9"),
                    Make = "Ford",
                    Model = "Mustang",
                    Year = 2023,
                    WltpMixed = 10.5f,
                    TankCapacity = 61f,
                    FuelType = FuelTypeEnum.Gasoline
                },
                // Nissan
                new CarModel
                {
                    Id = Guid.Parse("679318b7-6e4e-4f33-87b6-e2a7e7ed27ea"),
                    Make = "Nissan",
                    Model = "Leaf",
                    Year = 2023,
                    WltpMixed = 17.0f,
                    BatteryCapacity = 62f,
                    FuelType = FuelTypeEnum.Electric
                },
                new CarModel
                {
                    Id = Guid.Parse("779318b7-6e4e-4f33-87b6-e2a7e7ed27eb"),
                    Make = "Nissan",
                    Model = "Altima",
                    Year = 2023,
                    WltpMixed = 6.8f,
                    TankCapacity = 56f,
                    FuelType = FuelTypeEnum.Gasoline
                },
                // Hyundai
                new CarModel
                {
                    Id = Guid.Parse("879318b7-6e4e-4f33-87b6-e2a7e7ed27ec"),
                    Make = "Hyundai",
                    Model = "Elantra",
                    Year = 2023,
                    WltpMixed = 6.0f,
                    TankCapacity = 50f,
                    FuelType = FuelTypeEnum.Hybrid
                },
                new CarModel
                {
                    Id = Guid.Parse("979318b7-6e4e-4f33-87b6-e2a7e7ed27ed"),
                    Make = "Hyundai",
                    Model = "Ioniq 5",
                    Year = 2023,
                    WltpMixed = 18.5f,
                    BatteryCapacity = 77f,
                    FuelType = FuelTypeEnum.Electric
                },
                // Kia
                new CarModel
                {
                    Id = Guid.Parse("a79318b7-6e4e-4f33-87b6-e2a7e7ed27ee"),
                    Make = "Kia",
                    Model = "Sportage",
                    Year = 2023,
                    WltpMixed = 6.5f,
                    TankCapacity = 54f,
                    FuelType = FuelTypeEnum.Hybrid
                },
                new CarModel
                {
                    Id = Guid.Parse("b79318b7-6e4e-4f33-87b6-e2a7e7ed27ef"),
                    Make = "Kia",
                    Model = "EV6",
                    Year = 2023,
                    WltpMixed = 18.0f,
                    BatteryCapacity = 77f,
                    FuelType = FuelTypeEnum.Electric
                }
            );
        }
    }
}
