using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CookingSchoolASP.NET.Migrations
{
    /// <inheritdoc />
    public partial class ProjectFile1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FavoriteN = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHashed = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSlot = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PhoneNumber = table.Column<int>(type: "int", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedRefreshToken = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExpireRefreshToken = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Level = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CardN = table.Column<int>(type: "int", nullable: true),
                    CvPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FavoriteN = table.Column<int>(type: "int", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CookClass",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    ChefId = table.Column<int>(type: "int", nullable: false),
                    StartingAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndingAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AdminId = table.Column<int>(type: "int", nullable: true),
                    TraineeId = table.Column<int>(type: "int", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CookClass", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CookClass_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CookClass_User_AdminId",
                        column: x => x.AdminId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CookClass_User_ChefId",
                        column: x => x.ChefId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CookClass_User_TraineeId",
                        column: x => x.TraineeId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Favorite_chef",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TraineeId = table.Column<int>(type: "int", nullable: false),
                    ChefId = table.Column<int>(type: "int", nullable: false),
                    AdminId = table.Column<int>(type: "int", nullable: true),
                    ChefId1 = table.Column<int>(type: "int", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorite_chef", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Favorite_chef_User_AdminId",
                        column: x => x.AdminId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Favorite_chef_User_ChefId",
                        column: x => x.ChefId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Favorite_chef_User_ChefId1",
                        column: x => x.ChefId1,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Favorite_chef_User_TraineeId",
                        column: x => x.TraineeId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Favorite_Course",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TraineeId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    AdminId = table.Column<int>(type: "int", nullable: true),
                    ChefId = table.Column<int>(type: "int", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorite_Course", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Favorite_Course_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Favorite_Course_User_AdminId",
                        column: x => x.AdminId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Favorite_Course_User_ChefId",
                        column: x => x.ChefId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Favorite_Course_User_TraineeId",
                        column: x => x.TraineeId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FavoriteMeal_chef",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChefId = table.Column<int>(type: "int", nullable: false),
                    MealId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteMeal_chef", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FavoriteMeal_chef_User_ChefId",
                        column: x => x.ChefId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavoriteMeal_chef_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FavoriteMeal_Trainee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TraineeId = table.Column<int>(type: "int", nullable: false),
                    MealId = table.Column<int>(type: "int", nullable: false),
                    AdminId = table.Column<int>(type: "int", nullable: true),
                    ChefId = table.Column<int>(type: "int", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteMeal_Trainee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FavoriteMeal_Trainee_User_AdminId",
                        column: x => x.AdminId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FavoriteMeal_Trainee_User_ChefId",
                        column: x => x.ChefId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FavoriteMeal_Trainee_User_TraineeId",
                        column: x => x.TraineeId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trainee_Course",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TraineeId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    TotalMark = table.Column<int>(name: "Total_Mark", type: "int", nullable: true),
                    AdminId = table.Column<int>(type: "int", nullable: true),
                    ChefId = table.Column<int>(type: "int", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainee_Course", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trainee_Course_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trainee_Course_User_AdminId",
                        column: x => x.AdminId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Trainee_Course_User_ChefId",
                        column: x => x.ChefId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Trainee_Course_User_TraineeId",
                        column: x => x.TraineeId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Application",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TraineeId = table.Column<int>(type: "int", nullable: false),
                    CookClassId = table.Column<int>(type: "int", nullable: false),
                    DateOfApplay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Application", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Application_CookClass_CookClassId",
                        column: x => x.CookClassId,
                        principalTable: "CookClass",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Application_User_TraineeId",
                        column: x => x.TraineeId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassDays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Day = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CookClassId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassDays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassDays_CookClass_CookClassId",
                        column: x => x.CookClassId,
                        principalTable: "CookClass",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CookClassId = table.Column<int>(type: "int", nullable: false),
                    ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpirDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Project_CookClass_CookClassId",
                        column: x => x.CookClassId,
                        principalTable: "CookClass",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectFile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubmitedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TraineeId = table.Column<int>(type: "int", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    content = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
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
                    table.PrimaryKey("PK_ProjectFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectFile_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectFile_User_AdminId",
                        column: x => x.AdminId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectFile_User_ChefId",
                        column: x => x.ChefId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectFile_User_TraineeId",
                        column: x => x.TraineeId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Application_CookClassId",
                table: "Application",
                column: "CookClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Application_TraineeId",
                table: "Application",
                column: "TraineeId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassDays_CookClassId",
                table: "ClassDays",
                column: "CookClassId");

            migrationBuilder.CreateIndex(
                name: "IX_CookClass_AdminId",
                table: "CookClass",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_CookClass_ChefId",
                table: "CookClass",
                column: "ChefId");

            migrationBuilder.CreateIndex(
                name: "IX_CookClass_CourseId",
                table: "CookClass",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CookClass_TraineeId",
                table: "CookClass",
                column: "TraineeId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorite_chef_AdminId",
                table: "Favorite_chef",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorite_chef_ChefId",
                table: "Favorite_chef",
                column: "ChefId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorite_chef_ChefId1",
                table: "Favorite_chef",
                column: "ChefId1");

            migrationBuilder.CreateIndex(
                name: "IX_Favorite_chef_TraineeId",
                table: "Favorite_chef",
                column: "TraineeId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorite_Course_AdminId",
                table: "Favorite_Course",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorite_Course_ChefId",
                table: "Favorite_Course",
                column: "ChefId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorite_Course_CourseId",
                table: "Favorite_Course",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorite_Course_TraineeId",
                table: "Favorite_Course",
                column: "TraineeId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteMeal_chef_ChefId",
                table: "FavoriteMeal_chef",
                column: "ChefId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteMeal_chef_UserId",
                table: "FavoriteMeal_chef",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteMeal_Trainee_AdminId",
                table: "FavoriteMeal_Trainee",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteMeal_Trainee_ChefId",
                table: "FavoriteMeal_Trainee",
                column: "ChefId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteMeal_Trainee_TraineeId",
                table: "FavoriteMeal_Trainee",
                column: "TraineeId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_CookClassId",
                table: "Project",
                column: "CookClassId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectFile_AdminId",
                table: "ProjectFile",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectFile_ChefId",
                table: "ProjectFile",
                column: "ChefId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectFile_ProjectId",
                table: "ProjectFile",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectFile_TraineeId",
                table: "ProjectFile",
                column: "TraineeId");

            migrationBuilder.CreateIndex(
                name: "IX_Trainee_Course_AdminId",
                table: "Trainee_Course",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Trainee_Course_ChefId",
                table: "Trainee_Course",
                column: "ChefId");

            migrationBuilder.CreateIndex(
                name: "IX_Trainee_Course_CourseId",
                table: "Trainee_Course",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Trainee_Course_TraineeId",
                table: "Trainee_Course",
                column: "TraineeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Application");

            migrationBuilder.DropTable(
                name: "ClassDays");

            migrationBuilder.DropTable(
                name: "Favorite_chef");

            migrationBuilder.DropTable(
                name: "Favorite_Course");

            migrationBuilder.DropTable(
                name: "FavoriteMeal_chef");

            migrationBuilder.DropTable(
                name: "FavoriteMeal_Trainee");

            migrationBuilder.DropTable(
                name: "ProjectFile");

            migrationBuilder.DropTable(
                name: "Trainee_Course");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "CookClass");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
