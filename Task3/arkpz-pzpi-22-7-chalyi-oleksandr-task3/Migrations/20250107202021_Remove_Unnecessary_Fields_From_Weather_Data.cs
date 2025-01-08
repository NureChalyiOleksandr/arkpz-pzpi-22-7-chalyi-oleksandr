using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartLightSense.Migrations
{
    /// <inheritdoc />
    public partial class Remove_Unnecessary_Fields_From_Weather_Data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Precipitation",
                table: "WeatherData");

            migrationBuilder.DropColumn(
                name: "Temperature",
                table: "WeatherData");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Precipitation",
                table: "WeatherData",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Temperature",
                table: "WeatherData",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
