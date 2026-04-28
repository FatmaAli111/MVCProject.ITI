using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCProject.ITI.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedIdentityRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("46686121-d1c1-4796-993d-82d2a45a6660"),
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "fa02f2cd-1d44-4281-a37a-39754a010149", "f0883b27-c1d1-4e63-9993-82d2a45a6660" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("46686121-d1c1-4796-993d-82d2a45a6660"),
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "28c6e58f-dc8a-4292-bb20-96b8124c0a2c", "09dcdd2d-bc43-4252-84de-1b966e14cd93" });
        }
    }
}
