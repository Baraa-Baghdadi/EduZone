using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduZone.Migrations
{
    /// <inheritdoc />
    public partial class updateLessonEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "AppLessons",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "AppLessons",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "AppLessons",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "AppLessons",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "AppLessons",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExtraProperties",
                table: "AppLessons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "FileSize",
                table: "AppLessons",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AppLessons",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "AppLessons",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifierId",
                table: "AppLessons",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AppLessons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "AppLessons");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "AppLessons");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "AppLessons");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "AppLessons");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "AppLessons");

            migrationBuilder.DropColumn(
                name: "ExtraProperties",
                table: "AppLessons");

            migrationBuilder.DropColumn(
                name: "FileSize",
                table: "AppLessons");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AppLessons");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "AppLessons");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "AppLessons");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AppLessons");
        }
    }
}
