using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LoginPractice.Migrations
{
    /// <inheritdoc />
    public partial class AddToSeedDataInUserDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "UserDetails",
                columns: new[] { "Id", "City", "Country", "Created", "Name", "Updated" },
                values: new object[,]
                {
                    { 1, "Jamnagar", "India", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Vivek", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Ahmedabad", "India", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Meet", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "Hydrabad", "India", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dheeraj", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserDetails",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserDetails",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UserDetails",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
