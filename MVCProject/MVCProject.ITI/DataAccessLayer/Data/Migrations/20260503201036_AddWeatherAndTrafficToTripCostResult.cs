using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCProject.ITI.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddWeatherAndTrafficToTripCostResult : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TrafficCondition",
                table: "TripCostResults",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<float>(
                name: "TrafficMultiplier",
                table: "TripCostResults",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "WeatherCondition",
                table: "TripCostResults",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<float>(
                name: "WeatherMultiplier",
                table: "TripCostResults",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("46686121-d1c1-4796-993d-82d2a45a6660"),
                column: "ConcurrencyStamp",
                value: "f5c5239a-44c7-4f80-902b-aea119665d82");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrafficCondition",
                table: "TripCostResults");

            migrationBuilder.DropColumn(
                name: "TrafficMultiplier",
                table: "TripCostResults");

            migrationBuilder.DropColumn(
                name: "WeatherCondition",
                table: "TripCostResults");

            migrationBuilder.DropColumn(
                name: "WeatherMultiplier",
                table: "TripCostResults");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("46686121-d1c1-4796-993d-82d2a45a6660"),
                column: "ConcurrencyStamp",
                value: "9d51b261-bc5f-47f2-bd9f-3a61033e2afa");
        }
    }
}
