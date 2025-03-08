using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherArchives.Migrations
{
    /// <inheritdoc />
    public partial class UpdateWeatherRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "WindSpeed",
                table: "WeatherRecords",
                type: "integer",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<double>(
                name: "Humidity",
                table: "WeatherRecords",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "WeatherRecords",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "WeatherRecords",
                type: "timestamp",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<int>(
                name: "AirPressure",
                table: "WeatherRecords",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Clouds",
                table: "WeatherRecords",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "H",
                table: "WeatherRecords",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Td",
                table: "WeatherRecords",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "VV",
                table: "WeatherRecords",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "WindDirection",
                table: "WeatherRecords",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AirPressure",
                table: "WeatherRecords");

            migrationBuilder.DropColumn(
                name: "Clouds",
                table: "WeatherRecords");

            migrationBuilder.DropColumn(
                name: "H",
                table: "WeatherRecords");

            migrationBuilder.DropColumn(
                name: "Td",
                table: "WeatherRecords");

            migrationBuilder.DropColumn(
                name: "VV",
                table: "WeatherRecords");

            migrationBuilder.DropColumn(
                name: "WindDirection",
                table: "WeatherRecords");

            migrationBuilder.AlterColumn<double>(
                name: "WindSpeed",
                table: "WeatherRecords",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "Humidity",
                table: "WeatherRecords",
                type: "integer",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "WeatherRecords",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "WeatherRecords",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp");
        }
    }
}
