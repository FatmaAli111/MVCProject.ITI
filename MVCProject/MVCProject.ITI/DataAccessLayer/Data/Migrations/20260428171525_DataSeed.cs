using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MVCProject.ITI.Data.Migrations
{
    /// <inheritdoc />
    public partial class DataSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "FuelType", "Make", "Model", "WltpCity", "WltpHighway", "WltpMixed", "Year" },
                values: new object[,]
                {
                    { new Guid("13735163-952a-466d-8e7c-87d3dfa7263b"), 0, "Toyota", "Corolla", 5.5f, 4.2f, 4.9f, 2023 },
                    { new Guid("279318b7-6e4e-4f33-87b6-e2a7e7ed27d6"), 0, "Honda", "Civic", 6.2f, 4.8f, 5.5f, 2023 }
                });

            migrationBuilder.InsertData(
                table: "FuelPrices",
                columns: new[] { "Id", "Currency", "FuelType", "PricePerUnit", "RecordedDate", "Region" },
                values: new object[,]
                {
                    { new Guid("85623838-8a8b-4a5d-b088-25679eff9011"), "EGP", 0, 13.5f, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Egypt" },
                    { new Guid("a1d82f7c-50bc-4340-a3fc-211c4794e771"), "EGP", 1, 10f, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Egypt" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("d6e87f16-7788-4f10-9c1c-0c3f09f023ea"), null, "Admin", "ADMIN" },
                    { new Guid("f6e87f16-7788-4f10-9c1c-0c3f09f023eb"), null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("46686121-d1c1-4796-993d-82d2a45a6660"), 0, "28c6e58f-dc8a-4292-bb20-96b8124c0a2c", "admin@trips.com", true, false, null, "ADMIN@TRIPS.COM", "ADMIN@TRIPS.COM", "AQAAAAIAAYagAAAAEJ6Y8+qHjG/f/8+7G1Ww0W5f6+9Q5f6+9Q5f6+9Q5f6+9Q==", null, false, "09dcdd2d-bc43-4252-84de-1b966e14cd93", false, "admin@trips.com" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("d6e87f16-7788-4f10-9c1c-0c3f09f023ea"), new Guid("46686121-d1c1-4796-993d-82d2a45a6660") });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Id", "CarModelId", "NickName", "PassengerCapacity", "UserId" },
                values: new object[] { new Guid("f9b5a7a9-2f22-4a7b-a454-077a28424294"), new Guid("13735163-952a-466d-8e7c-87d3dfa7263b"), "Admin's Corolla", 5, new Guid("46686121-d1c1-4796-993d-82d2a45a6660") });

            migrationBuilder.InsertData(
                table: "FuelEfficiencyProfiles",
                columns: new[] { "Id", "ConsumptionRate", "DrivingCondation", "Unit", "VehicleId" },
                values: new object[] { new Guid("67890123-4567-8901-2345-678901234567"), 5f, 2, "L/100km", new Guid("f9b5a7a9-2f22-4a7b-a454-077a28424294") });

            migrationBuilder.InsertData(
                table: "Trips",
                columns: new[] { "Id", "CreatedAt", "DestinationName", "DestinationPlaceId", "DistanceKm", "DurationMinutes", "IsAcOn", "IsFavorite", "IsReturnTrip", "OriginName", "OriginPlaceId", "PassengerCount", "TripDate", "UserId", "VehicleId" },
                values: new object[] { new Guid("39542a3a-2a4c-4737-9755-e7a685764d26"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Alexandria", "", 220f, 180, false, false, false, "Cairo", "", 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("46686121-d1c1-4796-993d-82d2a45a6660"), new Guid("f9b5a7a9-2f22-4a7b-a454-077a28424294") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("279318b7-6e4e-4f33-87b6-e2a7e7ed27d6"));

            migrationBuilder.DeleteData(
                table: "FuelEfficiencyProfiles",
                keyColumn: "Id",
                keyValue: new Guid("67890123-4567-8901-2345-678901234567"));

            migrationBuilder.DeleteData(
                table: "FuelPrices",
                keyColumn: "Id",
                keyValue: new Guid("85623838-8a8b-4a5d-b088-25679eff9011"));

            migrationBuilder.DeleteData(
                table: "FuelPrices",
                keyColumn: "Id",
                keyValue: new Guid("a1d82f7c-50bc-4340-a3fc-211c4794e771"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("f6e87f16-7788-4f10-9c1c-0c3f09f023eb"));

            migrationBuilder.DeleteData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: new Guid("39542a3a-2a4c-4737-9755-e7a685764d26"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("d6e87f16-7788-4f10-9c1c-0c3f09f023ea"), new Guid("46686121-d1c1-4796-993d-82d2a45a6660") });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("d6e87f16-7788-4f10-9c1c-0c3f09f023ea"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("f9b5a7a9-2f22-4a7b-a454-077a28424294"));

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("13735163-952a-466d-8e7c-87d3dfa7263b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("46686121-d1c1-4796-993d-82d2a45a6660"));
        }
    }
}
