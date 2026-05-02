using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCProject.ITI.Data.Migrations
{
    /// <inheritdoc />
    public partial class addingBatteryCapacity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DrivingCondation",
                table: "FuelEfficiencyProfiles");

            migrationBuilder.AddColumn<float>(
                name: "BatteryCapacity",
                table: "Cars",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("13735163-952a-466d-8e7c-87d3dfa7263b"),
                column: "BatteryCapacity",
                value: 0f);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("279318b7-6e4e-4f33-87b6-e2a7e7ed27d6"),
                column: "BatteryCapacity",
                value: 0f);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("46686121-d1c1-4796-993d-82d2a45a6660"),
                column: "ConcurrencyStamp",
                value: "94c7bd35-0bd1-422f-968f-8df371b8bd7f");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BatteryCapacity",
                table: "Cars");

            migrationBuilder.AddColumn<int>(
                name: "DrivingCondation",
                table: "FuelEfficiencyProfiles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "FuelEfficiencyProfiles",
                keyColumn: "Id",
                keyValue: new Guid("67890123-4567-8901-2345-678901234567"),
                column: "DrivingCondation",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("46686121-d1c1-4796-993d-82d2a45a6660"),
                column: "ConcurrencyStamp",
                value: "eea8f14a-d366-422b-a8be-da2785a77aa2");
        }
    }
}
