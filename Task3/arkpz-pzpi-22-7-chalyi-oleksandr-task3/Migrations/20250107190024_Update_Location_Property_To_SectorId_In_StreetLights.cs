using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartLightSense.Migrations
{
    /// <inheritdoc />
    public partial class Update_Location_Property_To_SectorId_In_StreetLights : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Streetlights");

            migrationBuilder.AddColumn<int>(
                name: "SectorId",
                table: "Streetlights",
                type: "int",
                maxLength: 255,
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SectorId",
                table: "Streetlights");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Streetlights",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }
    }
}
