using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CookingSchoolASP.NET.Migrations
{
    /// <inheritdoc />
    public partial class setup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectFile_Project_ProjectId",
                table: "ProjectFile");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectFile_User_AdminId",
                table: "ProjectFile");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectFile_User_ChefId",
                table: "ProjectFile");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectFile_User_TraineeId",
                table: "ProjectFile");

            migrationBuilder.DropIndex(
                name: "IX_ProjectFile_AdminId",
                table: "ProjectFile");

            migrationBuilder.DropIndex(
                name: "IX_ProjectFile_ChefId",
                table: "ProjectFile");

            migrationBuilder.DropIndex(
                name: "IX_ProjectFile_TraineeId",
                table: "ProjectFile");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "ProjectFile");

            migrationBuilder.DropColumn(
                name: "ChefId",
                table: "ProjectFile");

            migrationBuilder.DropColumn(
                name: "Evalution",
                table: "ProjectFile");

            migrationBuilder.DropColumn(
                name: "TraineeId",
                table: "ProjectFile");

            migrationBuilder.DropColumn(
                name: "status",
                table: "ProjectFile");

            migrationBuilder.AlterColumn<string>(
                name: "ContentPath",
                table: "Project",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "SubmitedFile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TraineeId = table.Column<int>(type: "int", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    ContentPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Evalution = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdminId = table.Column<int>(type: "int", nullable: true),
                    ChefId = table.Column<int>(type: "int", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubmitedFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubmitedFile_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubmitedFile_User_AdminId",
                        column: x => x.AdminId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SubmitedFile_User_ChefId",
                        column: x => x.ChefId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SubmitedFile_User_TraineeId",
                        column: x => x.TraineeId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubmitedFile_AdminId",
                table: "SubmitedFile",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_SubmitedFile_ChefId",
                table: "SubmitedFile",
                column: "ChefId");

            migrationBuilder.CreateIndex(
                name: "IX_SubmitedFile_ProjectId",
                table: "SubmitedFile",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_SubmitedFile_TraineeId",
                table: "SubmitedFile",
                column: "TraineeId");

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

            migrationBuilder.DropTable(
                name: "SubmitedFile");

            migrationBuilder.AddColumn<int>(
                name: "AdminId",
                table: "ProjectFile",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ChefId",
                table: "ProjectFile",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Evalution",
                table: "ProjectFile",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TraineeId",
                table: "ProjectFile",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "ProjectFile",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "ContentPath",
                table: "Project",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectFile_AdminId",
                table: "ProjectFile",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectFile_ChefId",
                table: "ProjectFile",
                column: "ChefId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectFile_TraineeId",
                table: "ProjectFile",
                column: "TraineeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectFile_Project_ProjectId",
                table: "ProjectFile",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectFile_User_AdminId",
                table: "ProjectFile",
                column: "AdminId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectFile_User_ChefId",
                table: "ProjectFile",
                column: "ChefId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectFile_User_TraineeId",
                table: "ProjectFile",
                column: "TraineeId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
