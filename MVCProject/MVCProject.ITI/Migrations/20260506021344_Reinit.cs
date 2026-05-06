using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MVCProject.ITI.Migrations
{
    /// <inheritdoc />
    public partial class Reinit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Make = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    WltpMixed = table.Column<float>(type: "real", nullable: false),
                    TankCapacity = table.Column<float>(type: "real", nullable: true),
                    BatteryCapacity = table.Column<float>(type: "real", nullable: true),
                    FuelType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FuelPrices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    FuelType = table.Column<int>(type: "int", nullable: false),
                    Region = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PricePerUnit = table.Column<float>(type: "real", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecordedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuelPrices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CarModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NickName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ColorHex = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicles_Cars_CarModelId",
                        column: x => x.CarModelId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vehicles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FuelEfficiencyProfiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    VehicleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConsumptionRate = table.Column<float>(type: "real", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuelEfficiencyProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FuelEfficiencyProfiles_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trips",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VehicleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OriginPlaceId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OriginName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DestinationPlaceId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DestinationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DistanceKm = table.Column<float>(type: "real", nullable: false),
                    DurationMinutes = table.Column<int>(type: "int", nullable: false),
                    PassengerCount = table.Column<int>(type: "int", nullable: false),
                    IsReturnTrip = table.Column<bool>(type: "bit", nullable: false),
                    IsFavorite = table.Column<bool>(type: "bit", nullable: false),
                    IsAcOn = table.Column<bool>(type: "bit", nullable: false),
                    TripDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trips_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trips_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TripCostResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    TripId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FuelPriceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FuelConsumed = table.Column<float>(type: "real", nullable: false),
                    TotalCost = table.Column<float>(type: "real", nullable: false),
                    CostPerKm = table.Column<float>(type: "real", nullable: false),
                    CostPerPassenger = table.Column<float>(type: "real", nullable: false),
                    CalculatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    WeatherCondition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrafficCondition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WeatherMultiplier = table.Column<float>(type: "real", nullable: false),
                    TrafficMultiplier = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripCostResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TripCostResults_FuelPrices_FuelPriceId",
                        column: x => x.FuelPriceId,
                        principalTable: "FuelPrices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TripCostResults_Trips_TripId",
                        column: x => x.TripId,
                        principalTable: "Trips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TripPassengers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TripId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShareAmount = table.Column<float>(type: "real", nullable: false),
                    SharePercentage = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripPassengers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TripPassengers_Trips_TripId",
                        column: x => x.TripId,
                        principalTable: "Trips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "BatteryCapacity", "FuelType", "Make", "Model", "TankCapacity", "WltpMixed", "Year" },
                values: new object[,]
                {
                    { new Guid("079318b7-6e4e-4f33-87b6-e2a7e7ed27e4"), 90f, 2, "Mercedes", "EQE", null, 18f, 2023 },
                    { new Guid("13735163-952a-466d-8e7c-87d3dfa7263b"), null, 0, "Toyota", "Corolla", null, 4.9f, 2023 },
                    { new Guid("179318b7-6e4e-4f33-87b6-e2a7e7ed27e5"), null, 0, "Volkswagen", "Golf", 50f, 5.4f, 2023 },
                    { new Guid("279318b7-6e4e-4f33-87b6-e2a7e7ed27d6"), null, 0, "Honda", "Civic", null, 5.5f, 2023 },
                    { new Guid("279318b7-6e4e-4f33-87b6-e2a7e7ed27e6"), 82f, 2, "Volkswagen", "ID.4", null, 17.5f, 2023 },
                    { new Guid("379318b7-6e4e-4f33-87b6-e2a7e7ed27d7"), null, 3, "Toyota", "Camry", 55f, 5.8f, 2023 },
                    { new Guid("379318b7-6e4e-4f33-87b6-e2a7e7ed27e7"), null, 1, "Volkswagen", "Passat", 66f, 5.2f, 2023 },
                    { new Guid("479318b7-6e4e-4f33-87b6-e2a7e7ed27d8"), null, 3, "Toyota", "RAV4", 55f, 6.2f, 2023 },
                    { new Guid("479318b7-6e4e-4f33-87b6-e2a7e7ed27e8"), null, 0, "Ford", "Focus", 52f, 5.6f, 2023 },
                    { new Guid("579318b7-6e4e-4f33-87b6-e2a7e7ed27d9"), null, 3, "Toyota", "Prius", 43f, 4f, 2023 },
                    { new Guid("579318b7-6e4e-4f33-87b6-e2a7e7ed27e9"), null, 0, "Ford", "Mustang", 61f, 10.5f, 2023 },
                    { new Guid("679318b7-6e4e-4f33-87b6-e2a7e7ed27da"), null, 0, "Toyota", "Corolla", 50f, 5.1f, 2022 },
                    { new Guid("679318b7-6e4e-4f33-87b6-e2a7e7ed27ea"), 62f, 2, "Nissan", "Leaf", null, 17f, 2023 },
                    { new Guid("779318b7-6e4e-4f33-87b6-e2a7e7ed27db"), null, 0, "Toyota", "Corolla", 50f, 4.8f, 2024 },
                    { new Guid("779318b7-6e4e-4f33-87b6-e2a7e7ed27eb"), null, 0, "Nissan", "Altima", 56f, 6.8f, 2023 },
                    { new Guid("879318b7-6e4e-4f33-87b6-e2a7e7ed27dc"), null, 0, "Honda", "Civic", 47f, 5.7f, 2022 },
                    { new Guid("879318b7-6e4e-4f33-87b6-e2a7e7ed27ec"), null, 3, "Hyundai", "Elantra", 50f, 6f, 2023 },
                    { new Guid("979318b7-6e4e-4f33-87b6-e2a7e7ed27dd"), null, 0, "Honda", "Civic", 47f, 5.3f, 2024 },
                    { new Guid("979318b7-6e4e-4f33-87b6-e2a7e7ed27ed"), 77f, 2, "Hyundai", "Ioniq 5", null, 18.5f, 2023 },
                    { new Guid("a79318b7-6e4e-4f33-87b6-e2a7e7ed27de"), null, 3, "Honda", "Accord", 48f, 6f, 2023 },
                    { new Guid("a79318b7-6e4e-4f33-87b6-e2a7e7ed27ee"), null, 3, "Kia", "Sportage", 54f, 6.5f, 2023 },
                    { new Guid("b79318b7-6e4e-4f33-87b6-e2a7e7ed27df"), null, 3, "Honda", "CR-V", 53f, 6.8f, 2023 },
                    { new Guid("b79318b7-6e4e-4f33-87b6-e2a7e7ed27ef"), 77f, 2, "Kia", "EV6", null, 18f, 2023 },
                    { new Guid("c79318b7-6e4e-4f33-87b6-e2a7e7ed27e0"), null, 1, "BMW", "3 Series", 59f, 5.9f, 2023 },
                    { new Guid("d79318b7-6e4e-4f33-87b6-e2a7e7ed27e1"), null, 0, "BMW", "3 Series", 59f, 6.5f, 2023 },
                    { new Guid("e79318b7-6e4e-4f33-87b6-e2a7e7ed27e2"), 84f, 2, "BMW", "i4", null, 16.5f, 2023 },
                    { new Guid("f79318b7-6e4e-4f33-87b6-e2a7e7ed27e3"), null, 1, "Mercedes", "C-Class", 66f, 6.2f, 2023 }
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
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("46686121-d1c1-4796-993d-82d2a45a6660"), 0, "a215a5ed-ed8e-415b-b9b0-33ac7aa13b28", "admin@trips.com", true, "", false, null, "ADMIN@TRIPS.COM", "ADMIN@TRIPS.COM", "AQAAAAIAAYagAAAAEJ6Y8+qHjG/f/8+7G1Ww0W5f6+9Q5f6+9Q5f6+9Q5f6+9Q==", null, false, "f0883b27-c1d1-4e63-9993-82d2a45a6660", false, "admin@trips.com" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("d6e87f16-7788-4f10-9c1c-0c3f09f023ea"), new Guid("46686121-d1c1-4796-993d-82d2a45a6660") });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Id", "CarModelId", "ColorHex", "IsDefault", "NickName", "UserId" },
                values: new object[] { new Guid("f9b5a7a9-2f22-4a7b-a454-077a28424294"), new Guid("13735163-952a-466d-8e7c-87d3dfa7263b"), "#800000", false, "Admin's Corolla", new Guid("46686121-d1c1-4796-993d-82d2a45a6660") });

            migrationBuilder.InsertData(
                table: "FuelEfficiencyProfiles",
                columns: new[] { "Id", "ConsumptionRate", "Unit", "VehicleId" },
                values: new object[] { new Guid("67890123-4567-8901-2345-678901234567"), 5f, "L/100km", new Guid("f9b5a7a9-2f22-4a7b-a454-077a28424294") });

            migrationBuilder.InsertData(
                table: "Trips",
                columns: new[] { "Id", "CreatedAt", "DestinationName", "DestinationPlaceId", "DistanceKm", "DurationMinutes", "IsAcOn", "IsFavorite", "IsReturnTrip", "OriginName", "OriginPlaceId", "PassengerCount", "TripDate", "UserId", "VehicleId" },
                values: new object[] { new Guid("39542a3a-2a4c-4737-9755-e7a685764d26"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Alexandria", "", 220f, 180, false, false, false, "Cairo", "", 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("46686121-d1c1-4796-993d-82d2a45a6660"), new Guid("f9b5a7a9-2f22-4a7b-a454-077a28424294") });

            migrationBuilder.CreateIndex(
                name: "IX_FuelEfficiencyProfiles_VehicleId",
                table: "FuelEfficiencyProfiles",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TripCostResults_FuelPriceId",
                table: "TripCostResults",
                column: "FuelPriceId");

            migrationBuilder.CreateIndex(
                name: "IX_TripCostResults_TripId",
                table: "TripCostResults",
                column: "TripId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TripPassengers_TripId",
                table: "TripPassengers",
                column: "TripId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_UserId",
                table: "Trips",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_VehicleId",
                table: "Trips",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_CarModelId",
                table: "Vehicles",
                column: "CarModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_UserId",
                table: "Vehicles",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FuelEfficiencyProfiles");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "TripCostResults");

            migrationBuilder.DropTable(
                name: "TripPassengers");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "FuelPrices");

            migrationBuilder.DropTable(
                name: "Trips");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
