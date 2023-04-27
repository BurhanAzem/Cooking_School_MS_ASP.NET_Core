using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CookingSchoolASP.NET.Migrations
{
    /// <inheritdoc />
    public partial class AddCourseLevel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourseLevel",
                table: "Course",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectFile_Project_ProjectId",
                table: "ProjectFile",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectFile_Project_ProjectId",
                table: "ProjectFile");

            migrationBuilder.DropColumn(
                name: "CourseLevel",
                table: "Course");
        }
    }
}
