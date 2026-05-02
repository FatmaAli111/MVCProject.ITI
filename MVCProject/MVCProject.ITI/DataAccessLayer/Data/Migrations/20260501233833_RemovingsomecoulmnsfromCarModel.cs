using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCProject.ITI.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemovingsomecoulmnsfromCarModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("46686121-d1c1-4796-993d-82d2a45a6660"),
                column: "ConcurrencyStamp",
                value: "12626175-935f-44eb-af9a-c4da602552d9");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("46686121-d1c1-4796-993d-82d2a45a6660"),
                column: "ConcurrencyStamp",
                value: "94c7bd35-0bd1-422f-968f-8df371b8bd7f");
        }
    }
}
