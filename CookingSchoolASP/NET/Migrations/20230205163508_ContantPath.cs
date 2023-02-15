using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CookingSchoolASP.NET.Migrations
{
    /// <inheritdoc />
    public partial class ContantPath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "content",
                table: "ProjectFile");

            migrationBuilder.AddColumn<string>(
                name: "ContentPath",
                table: "ProjectFile",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentPath",
                table: "ProjectFile");

            migrationBuilder.AddColumn<byte[]>(
                name: "content",
                table: "ProjectFile",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
