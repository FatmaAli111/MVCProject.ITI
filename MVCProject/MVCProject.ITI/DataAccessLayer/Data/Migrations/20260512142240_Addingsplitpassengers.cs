using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCProject.ITI.DataAccessLayer.Data.Migrations
{
    /// <inheritdoc />
    public partial class Addingsplitpassengers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("46686121-d1c1-4796-993d-82d2a45a6660"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "7ff4f428-f08b-4fa7-b3ad-56f855f816dc", "AQAAAAIAAYagAAAAEByZEhQ71Szcs520Kans43pdJHRw7yCnDgDEPVb+45vQ9ky5N3u5MAn0Px8jfpjmzA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("46686121-d1c1-4796-993d-82d2a45a6660"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b23a7d9b-056b-4e4f-b570-930d05cad0e0", "AQAAAAIAAYagAAAAEPLGqFdsdjd6ybIdvmSKlkO7oWZ+M6//8CLYWNgfzQM4gTa8mhjZvzA53irJ/xbfbA==" });
        }
    }
}
