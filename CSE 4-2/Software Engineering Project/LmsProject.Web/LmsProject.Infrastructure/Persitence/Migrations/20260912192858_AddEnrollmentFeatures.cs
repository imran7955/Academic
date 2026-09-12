using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LmsProject.Infrastructure.Persitence.Migrations
{
    /// <inheritdoc />
    public partial class AddEnrollmentFeatures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EnrollmentDeadline",
                table: "Courses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "EnrollmentStartDate",
                table: "Courses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "CourseUserProfile",
                columns: table => new
                {
                    EnrolledCoursesId = table.Column<int>(type: "int", nullable: false),
                    EnrolledUsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseUserProfile", x => new { x.EnrolledCoursesId, x.EnrolledUsersId });
                    table.ForeignKey(
                        name: "FK_CourseUserProfile_Courses_EnrolledCoursesId",
                        column: x => x.EnrolledCoursesId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseUserProfile_UserProfiles_EnrolledUsersId",
                        column: x => x.EnrolledUsersId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseUserProfile_EnrolledUsersId",
                table: "CourseUserProfile",
                column: "EnrolledUsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseUserProfile");

            migrationBuilder.DropColumn(
                name: "EnrollmentDeadline",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "EnrollmentStartDate",
                table: "Courses");
        }
    }
}
