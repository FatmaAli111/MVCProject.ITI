using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCProject.ITI.DataAccessLayer.Data.Migrations
{
    /// <inheritdoc />
    public partial class Admin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("46686121-d1c1-4796-993d-82d2a45a6660"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d23207c7-0cfd-409e-8739-ecd75598dc69", "AQAAAAIAAYagAAAAEM1/4144nzBo00dFNOjP6UyxrkFPYC8Bo1my1xLBCL1KlDzQzdJYH8rQJdV06ToLng==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("46686121-d1c1-4796-993d-82d2a45a6660"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f8a42f85-f4f1-4836-9437-0b42ef3e8715", "AQAAAAIAAYagAAAAEN1piYDx3CjftcjAxr96qA7Ybz6C907ZpZdkF0F3oQfIYgTako8Hg9S/owmTk7zpYw==" });
        }
    }
}
