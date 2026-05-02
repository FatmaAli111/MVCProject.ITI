using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCProject.ITI.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixPendingWarning : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("46686121-d1c1-4796-993d-82d2a45a6660"),
                column: "ConcurrencyStamp",
                value: "58c42c13-eb96-4168-a786-1c962545c94e");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("46686121-d1c1-4796-993d-82d2a45a6660"),
                column: "ConcurrencyStamp",
                value: "c7d8f0ec-d7f6-4f82-9746-3e4790f94e19");
        }
    }
}
