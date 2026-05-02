using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCProject.ITI.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddFullNameToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("46686121-d1c1-4796-993d-82d2a45a6660"),
                columns: new[] { "ConcurrencyStamp", "FullName" },
                values: new object[] { "f31d6d42-c2b1-423a-92fe-f99b5bd64c18", "" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("46686121-d1c1-4796-993d-82d2a45a6660"),
                column: "ConcurrencyStamp",
                value: "58c42c13-eb96-4168-a786-1c962545c94e");
        }
    }
}
