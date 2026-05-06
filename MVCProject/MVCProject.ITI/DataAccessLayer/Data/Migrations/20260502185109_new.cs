using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCProject.ITI.Data.Migrations
{
    /// <inheritdoc />
    public partial class @new : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("46686121-d1c1-4796-993d-82d2a45a6660"),
                column: "ConcurrencyStamp",
                value: "7172a7b9-f986-4fe5-9935-67d1695ab292");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("46686121-d1c1-4796-993d-82d2a45a6660"),
                column: "ConcurrencyStamp",
                value: "9d51b261-bc5f-47f2-bd9f-3a61033e2afa");
        }
    }
}
