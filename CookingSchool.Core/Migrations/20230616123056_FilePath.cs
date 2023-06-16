using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CookingSchool.Core.Migrations
{
    /// <inheritdoc />
    public partial class FilePath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubmitedFiles");

            migrationBuilder.RenameColumn(
                name: "ContentPath",
                table: "ProjectFiles",
                newName: "FilePath");

            migrationBuilder.CreateTable(
                name: "ProjectTraineeFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectTraineeId = table.Column<int>(type: "int", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTraineeFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectTraineeFiles_ProjectTrainees_ProjectTraineeId",
                        column: x => x.ProjectTraineeId,
                        principalTable: "ProjectTrainees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTraineeFiles_ProjectTraineeId",
                table: "ProjectTraineeFiles",
                column: "ProjectTraineeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectTraineeFiles");

            migrationBuilder.RenameColumn(
                name: "FilePath",
                table: "ProjectFiles",
                newName: "ContentPath");

            migrationBuilder.CreateTable(
                name: "SubmitedFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectTraineeId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<int>(type: "int", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubmitedFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubmitedFiles_ProjectTrainees_ProjectTraineeId",
                        column: x => x.ProjectTraineeId,
                        principalTable: "ProjectTrainees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubmitedFiles_ProjectTraineeId",
                table: "SubmitedFiles",
                column: "ProjectTraineeId");
        }
    }
}
