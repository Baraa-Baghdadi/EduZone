using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduZone.Migrations
{
    /// <inheritdoc />
    public partial class addLicenseEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LicenseId",
                table: "AppInstructors",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AppLicenses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppLicenses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppInstructors_LicenseId",
                table: "AppInstructors",
                column: "LicenseId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppInstructors_AppLicenses_LicenseId",
                table: "AppInstructors",
                column: "LicenseId",
                principalTable: "AppLicenses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppInstructors_AppLicenses_LicenseId",
                table: "AppInstructors");

            migrationBuilder.DropTable(
                name: "AppLicenses");

            migrationBuilder.DropIndex(
                name: "IX_AppInstructors_LicenseId",
                table: "AppInstructors");

            migrationBuilder.DropColumn(
                name: "LicenseId",
                table: "AppInstructors");
        }
    }
}
