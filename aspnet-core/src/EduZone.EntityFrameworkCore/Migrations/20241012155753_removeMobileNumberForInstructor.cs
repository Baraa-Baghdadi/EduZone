using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduZone.Migrations
{
    /// <inheritdoc />
    public partial class removeMobileNumberForInstructor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountryCode",
                table: "AppInstructors");

            migrationBuilder.DropColumn(
                name: "FullMobileNumber",
                table: "AppInstructors");

            migrationBuilder.DropColumn(
                name: "MobileNumber",
                table: "AppInstructors");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CountryCode",
                table: "AppInstructors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FullMobileNumber",
                table: "AppInstructors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MobileNumber",
                table: "AppInstructors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
