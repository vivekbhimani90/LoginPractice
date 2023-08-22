using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoginPractice.Migrations
{
    /// <inheritdoc />
    public partial class AddToFileInNewProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Files",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PersonName",
                table: "Files",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "PersonName",
                table: "Files");
        }
    }
}
