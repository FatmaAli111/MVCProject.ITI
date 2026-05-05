using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MVCProject.ITI.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCarModelSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "UserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "UserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "UserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "UserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "BatteryCapacity", "FuelType", "Make", "Model", "TankCapacity", "WltpMixed", "Year" },
                values: new object[,]
                {
                    { new Guid("079318b7-6e4e-4f33-87b6-e2a7e7ed27e4"), 90f, 2, "Mercedes", "EQE", null, 18f, 2023 },
                    { new Guid("179318b7-6e4e-4f33-87b6-e2a7e7ed27e5"), null, 0, "Volkswagen", "Golf", 50f, 5.4f, 2023 },
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

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("46686121-d1c1-4796-993d-82d2a45a6660"),
                column: "ConcurrencyStamp",
                value: "4b1f7d9c-32bb-4697-9b27-6b293386ab8c");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("079318b7-6e4e-4f33-87b6-e2a7e7ed27e4"));

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("179318b7-6e4e-4f33-87b6-e2a7e7ed27e5"));

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("279318b7-6e4e-4f33-87b6-e2a7e7ed27e6"));

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("379318b7-6e4e-4f33-87b6-e2a7e7ed27d7"));

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("379318b7-6e4e-4f33-87b6-e2a7e7ed27e7"));

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("479318b7-6e4e-4f33-87b6-e2a7e7ed27d8"));

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("479318b7-6e4e-4f33-87b6-e2a7e7ed27e8"));

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("579318b7-6e4e-4f33-87b6-e2a7e7ed27d9"));

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("579318b7-6e4e-4f33-87b6-e2a7e7ed27e9"));

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("679318b7-6e4e-4f33-87b6-e2a7e7ed27da"));

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("679318b7-6e4e-4f33-87b6-e2a7e7ed27ea"));

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("779318b7-6e4e-4f33-87b6-e2a7e7ed27db"));

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("779318b7-6e4e-4f33-87b6-e2a7e7ed27eb"));

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("879318b7-6e4e-4f33-87b6-e2a7e7ed27dc"));

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("879318b7-6e4e-4f33-87b6-e2a7e7ed27ec"));

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("979318b7-6e4e-4f33-87b6-e2a7e7ed27dd"));

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("979318b7-6e4e-4f33-87b6-e2a7e7ed27ed"));

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("a79318b7-6e4e-4f33-87b6-e2a7e7ed27de"));

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("a79318b7-6e4e-4f33-87b6-e2a7e7ed27ee"));

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("b79318b7-6e4e-4f33-87b6-e2a7e7ed27df"));

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("b79318b7-6e4e-4f33-87b6-e2a7e7ed27ef"));

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("c79318b7-6e4e-4f33-87b6-e2a7e7ed27e0"));

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("d79318b7-6e4e-4f33-87b6-e2a7e7ed27e1"));

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("e79318b7-6e4e-4f33-87b6-e2a7e7ed27e2"));

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("f79318b7-6e4e-4f33-87b6-e2a7e7ed27e3"));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "UserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "UserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "UserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "UserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("46686121-d1c1-4796-993d-82d2a45a6660"),
                column: "ConcurrencyStamp",
                value: "9d51b261-bc5f-47f2-bd9f-3a61033e2afa");
        }
    }
}
