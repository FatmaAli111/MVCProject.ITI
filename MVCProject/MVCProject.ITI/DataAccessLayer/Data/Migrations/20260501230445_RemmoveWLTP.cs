using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCProject.ITI.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemmoveWLTP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PassengerCapacity",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "WltpCity",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "WltpHighway",
                table: "Cars");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("46686121-d1c1-4796-993d-82d2a45a6660"),
                column: "ConcurrencyStamp",
                value: "eea8f14a-d366-422b-a8be-da2785a77aa2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PassengerCapacity",
                table: "Vehicles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "WltpCity",
                table: "Cars",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "WltpHighway",
                table: "Cars",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("13735163-952a-466d-8e7c-87d3dfa7263b"),
                columns: new[] { "WltpCity", "WltpHighway" },
                values: new object[] { 5.5f, 4.2f });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("279318b7-6e4e-4f33-87b6-e2a7e7ed27d6"),
                columns: new[] { "WltpCity", "WltpHighway" },
                values: new object[] { 6.2f, 4.8f });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("46686121-d1c1-4796-993d-82d2a45a6660"),
                column: "ConcurrencyStamp",
                value: "23e4a7b6-b4fb-423d-a7da-24dcac0032d3");

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("f9b5a7a9-2f22-4a7b-a454-077a28424294"),
                column: "PassengerCapacity",
                value: 5);
        }
    }
}
