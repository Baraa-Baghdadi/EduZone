using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduZone.Migrations
{
    /// <inheritdoc />
    public partial class testMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "CompletionDate",
                table: "AppEnrollments",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.CreateIndex(
                name: "IX_AppReviews_CourseId",
                table: "AppReviews",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_AppReviews_StudentId",
                table: "AppReviews",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppReviews_AppCourses_CourseId",
                table: "AppReviews",
                column: "CourseId",
                principalTable: "AppCourses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppReviews_AppStudents_StudentId",
                table: "AppReviews",
                column: "StudentId",
                principalTable: "AppStudents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppReviews_AppCourses_CourseId",
                table: "AppReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_AppReviews_AppStudents_StudentId",
                table: "AppReviews");

            migrationBuilder.DropIndex(
                name: "IX_AppReviews_CourseId",
                table: "AppReviews");

            migrationBuilder.DropIndex(
                name: "IX_AppReviews_StudentId",
                table: "AppReviews");

            migrationBuilder.AlterColumn<decimal>(
                name: "CompletionDate",
                table: "AppEnrollments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);
        }
    }
}
