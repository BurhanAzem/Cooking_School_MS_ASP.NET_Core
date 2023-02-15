﻿// <auto-generated />
using System;
using Cooking_School_ASP.NET.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CookingSchoolASP.NET.Migrations
{
    [DbContext(typeof(DBContext))]
    [Migration("20230205155847_ProjectFile")]
    partial class ProjectFile1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Backend_Controller_Burhan.Models.CookClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AdminId")
                        .HasColumnType("int");

                    b.Property<int>("ChefId")
                        .HasColumnType("int");

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int>("Deleted")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndingAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartingAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("TraineeId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AdminId");

                    b.HasIndex("ChefId");

                    b.HasIndex("CourseId");

                    b.HasIndex("TraineeId");

                    b.ToTable("CookClass", (string)null);
                });

            modelBuilder.Entity("Backend_Controller_Burhan.Models.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CourseName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int>("Deleted")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FavoriteN")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Course", (string)null);
                });

            modelBuilder.Entity("Cooking_School_ASP.NET.Models.ApplicationT", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CookClassId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfApplay")
                        .HasColumnType("datetime2");

                    b.Property<int>("Deleted")
                        .HasColumnType("int");

                    b.Property<int>("TraineeId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime2");

                    b.Property<string>("status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CookClassId");

                    b.HasIndex("TraineeId");

                    b.ToTable("Application", (string)null);
                });

            modelBuilder.Entity("Cooking_School_ASP.NET.Models.ClassDays", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CookClassId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Day")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Deleted")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CookClassId");

                    b.ToTable("ClassDays", (string)null);
                });

            modelBuilder.Entity("Cooking_School_ASP.NET.Models.FavoriteMeal_Trainee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AdminId")
                        .HasColumnType("int");

                    b.Property<int?>("ChefId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int>("Deleted")
                        .HasColumnType("int");

                    b.Property<int>("MealId")
                        .HasColumnType("int");

                    b.Property<int>("TraineeId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AdminId");

                    b.HasIndex("ChefId");

                    b.HasIndex("TraineeId");

                    b.ToTable("FavoriteMeal_Trainee", (string)null);
                });

            modelBuilder.Entity("Cooking_School_ASP.NET.Models.FavoriteMeal_chef", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ChefId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int>("Deleted")
                        .HasColumnType("int");

                    b.Property<int>("MealId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ChefId");

                    b.HasIndex("UserId");

                    b.ToTable("FavoriteMeal_chef", (string)null);
                });

            modelBuilder.Entity("Cooking_School_ASP.NET.Models.Favorite_Chef", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AdminId")
                        .HasColumnType("int");

                    b.Property<int>("ChefId")
                        .HasColumnType("int");

                    b.Property<int?>("ChefId1")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int>("Deleted")
                        .HasColumnType("int");

                    b.Property<int>("TraineeId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AdminId");

                    b.HasIndex("ChefId");

                    b.HasIndex("ChefId1");

                    b.HasIndex("TraineeId");

                    b.ToTable("Favorite_chef", (string)null);
                });

            modelBuilder.Entity("Cooking_School_ASP.NET.Models.Favorite_Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AdminId")
                        .HasColumnType("int");

                    b.Property<int?>("ChefId")
                        .HasColumnType("int");

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int>("Deleted")
                        .HasColumnType("int");

                    b.Property<int>("TraineeId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AdminId");

                    b.HasIndex("ChefId");

                    b.HasIndex("CourseId");

                    b.HasIndex("TraineeId");

                    b.ToTable("Favorite_Course", (string)null);
                });

            modelBuilder.Entity("Cooking_School_ASP.NET.Models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CookClassId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int>("Deleted")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ExpirDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ProjectName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CookClassId");

                    b.ToTable("Project", (string)null);
                });

            modelBuilder.Entity("Cooking_School_ASP.NET.Models.Trainee_Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AdminId")
                        .HasColumnType("int");

                    b.Property<int?>("ChefId")
                        .HasColumnType("int");

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int>("Deleted")
                        .HasColumnType("int");

                    b.Property<int?>("Total_Mark")
                        .HasColumnType("int");

                    b.Property<int>("TraineeId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AdminId");

                    b.HasIndex("ChefId");

                    b.HasIndex("CourseId");

                    b.HasIndex("TraineeId");

                    b.ToTable("Trainee_Course", (string)null);
                });

            modelBuilder.Entity("Cooking_School_ASP.NET.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("CreatedRefreshToken")
                        .HasColumnType("datetime2");

                    b.Property<int>("Deleted")
                        .HasColumnType("int");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ExpireRefreshToken")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHashed")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSlot")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("PhoneNumber")
                        .HasColumnType("int");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("User", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Cooking_School_ASP.NET_.Models.ProjectFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AdminId")
                        .HasColumnType("int");

                    b.Property<int?>("ChefId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int>("Deleted")
                        .HasColumnType("int");

                    b.Property<decimal?>("Evalution")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("SubmitedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("TraineeId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("content")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AdminId");

                    b.HasIndex("ChefId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("TraineeId");

                    b.ToTable("ProjectFile", (string)null);
                });

            modelBuilder.Entity("Backend_Controller_Burhan.Models.Trainee", b =>
                {
                    b.HasBaseType("Cooking_School_ASP.NET.Models.User");

                    b.Property<int>("CardN")
                        .HasColumnType("int");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Level")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Trainee");
                });

            modelBuilder.Entity("Cooking_School_ASP.NET.Models.Admin", b =>
                {
                    b.HasBaseType("Cooking_School_ASP.NET.Models.User");

                    b.HasDiscriminator().HasValue("Administrator");
                });

            modelBuilder.Entity("Cooking_School_ASP.NET.Models.Chef", b =>
                {
                    b.HasBaseType("Cooking_School_ASP.NET.Models.User");

                    b.Property<string>("CvPath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FavoriteN")
                        .HasColumnType("int");

                    b.Property<decimal>("Salary")
                        .HasColumnType("decimal(18,2)");

                    b.HasDiscriminator().HasValue("Chef");
                });

            modelBuilder.Entity("Backend_Controller_Burhan.Models.CookClass", b =>
                {
                    b.HasOne("Cooking_School_ASP.NET.Models.Admin", null)
                        .WithMany("CookClasses")
                        .HasForeignKey("AdminId");

                    b.HasOne("Cooking_School_ASP.NET.Models.Chef", "Chef")
                        .WithMany("CookClasses")
                        .HasForeignKey("ChefId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Backend_Controller_Burhan.Models.Course", "Course")
                        .WithMany("CookClasses")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Backend_Controller_Burhan.Models.Trainee", null)
                        .WithMany("CookClasses")
                        .HasForeignKey("TraineeId");

                    b.Navigation("Chef");

                    b.Navigation("Course");
                });

            modelBuilder.Entity("Cooking_School_ASP.NET.Models.ApplicationT", b =>
                {
                    b.HasOne("Backend_Controller_Burhan.Models.CookClass", "CookClass")
                        .WithMany("Applications")
                        .HasForeignKey("CookClassId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Backend_Controller_Burhan.Models.Trainee", "Trainee")
                        .WithMany()
                        .HasForeignKey("TraineeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CookClass");

                    b.Navigation("Trainee");
                });

            modelBuilder.Entity("Cooking_School_ASP.NET.Models.ClassDays", b =>
                {
                    b.HasOne("Backend_Controller_Burhan.Models.CookClass", "CookClass")
                        .WithMany("ClassDays")
                        .HasForeignKey("CookClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CookClass");
                });

            modelBuilder.Entity("Cooking_School_ASP.NET.Models.FavoriteMeal_Trainee", b =>
                {
                    b.HasOne("Cooking_School_ASP.NET.Models.Admin", null)
                        .WithMany("FavoriteMealTrainees")
                        .HasForeignKey("AdminId");

                    b.HasOne("Cooking_School_ASP.NET.Models.Chef", null)
                        .WithMany("FavoriteMealTrainees")
                        .HasForeignKey("ChefId");

                    b.HasOne("Backend_Controller_Burhan.Models.Trainee", "Trainee")
                        .WithMany("FavoriteMealTrainees")
                        .HasForeignKey("TraineeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Trainee");
                });

            modelBuilder.Entity("Cooking_School_ASP.NET.Models.FavoriteMeal_chef", b =>
                {
                    b.HasOne("Cooking_School_ASP.NET.Models.Chef", "Chef")
                        .WithMany("FavoriteMealchef")
                        .HasForeignKey("ChefId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cooking_School_ASP.NET.Models.User", null)
                        .WithMany("FavoriteMealchefs")
                        .HasForeignKey("UserId");

                    b.Navigation("Chef");
                });

            modelBuilder.Entity("Cooking_School_ASP.NET.Models.Favorite_Chef", b =>
                {
                    b.HasOne("Cooking_School_ASP.NET.Models.Admin", null)
                        .WithMany("FavoriteChefs")
                        .HasForeignKey("AdminId");

                    b.HasOne("Cooking_School_ASP.NET.Models.Chef", "Chef")
                        .WithMany("FavoriteChef")
                        .HasForeignKey("ChefId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Cooking_School_ASP.NET.Models.Chef", null)
                        .WithMany("FavoriteChefs")
                        .HasForeignKey("ChefId1");

                    b.HasOne("Backend_Controller_Burhan.Models.Trainee", "Trainee")
                        .WithMany("FavoriteChefs")
                        .HasForeignKey("TraineeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Chef");

                    b.Navigation("Trainee");
                });

            modelBuilder.Entity("Cooking_School_ASP.NET.Models.Favorite_Course", b =>
                {
                    b.HasOne("Cooking_School_ASP.NET.Models.Admin", null)
                        .WithMany("FavoriteCourses")
                        .HasForeignKey("AdminId");

                    b.HasOne("Cooking_School_ASP.NET.Models.Chef", null)
                        .WithMany("FavoriteCourses")
                        .HasForeignKey("ChefId");

                    b.HasOne("Backend_Controller_Burhan.Models.Course", "Course")
                        .WithMany("FavoriteCourse")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Backend_Controller_Burhan.Models.Trainee", "Trainee")
                        .WithMany("FavoriteCourses")
                        .HasForeignKey("TraineeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Trainee");
                });

            modelBuilder.Entity("Cooking_School_ASP.NET.Models.Project", b =>
                {
                    b.HasOne("Backend_Controller_Burhan.Models.CookClass", "CookClass")
                        .WithMany("Projects")
                        .HasForeignKey("CookClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CookClass");
                });

            modelBuilder.Entity("Cooking_School_ASP.NET.Models.Trainee_Course", b =>
                {
                    b.HasOne("Cooking_School_ASP.NET.Models.Admin", null)
                        .WithMany("TraineeCourses")
                        .HasForeignKey("AdminId");

                    b.HasOne("Cooking_School_ASP.NET.Models.Chef", null)
                        .WithMany("TraineeCourses")
                        .HasForeignKey("ChefId");

                    b.HasOne("Backend_Controller_Burhan.Models.Course", "Course")
                        .WithMany("TraineeCourse")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Backend_Controller_Burhan.Models.Trainee", "Trainee")
                        .WithMany("TraineeCourses")
                        .HasForeignKey("TraineeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Trainee");
                });

            modelBuilder.Entity("Cooking_School_ASP.NET_.Models.ProjectFile", b =>
                {
                    b.HasOne("Cooking_School_ASP.NET.Models.Admin", null)
                        .WithMany("ProjectFiles")
                        .HasForeignKey("AdminId");

                    b.HasOne("Cooking_School_ASP.NET.Models.Chef", null)
                        .WithMany("ProjectFiles")
                        .HasForeignKey("ChefId");

                    b.HasOne("Cooking_School_ASP.NET.Models.Project", "Project")
                        .WithMany("projectFiles")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Backend_Controller_Burhan.Models.Trainee", "Trainee")
                        .WithMany("ProjectFiles")
                        .HasForeignKey("TraineeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");

                    b.Navigation("Trainee");
                });

            modelBuilder.Entity("Backend_Controller_Burhan.Models.CookClass", b =>
                {
                    b.Navigation("Applications");

                    b.Navigation("ClassDays");

                    b.Navigation("Projects");
                });

            modelBuilder.Entity("Backend_Controller_Burhan.Models.Course", b =>
                {
                    b.Navigation("CookClasses");

                    b.Navigation("FavoriteCourse");

                    b.Navigation("TraineeCourse");
                });

            modelBuilder.Entity("Cooking_School_ASP.NET.Models.Project", b =>
                {
                    b.Navigation("projectFiles");
                });

            modelBuilder.Entity("Cooking_School_ASP.NET.Models.User", b =>
                {
                    b.Navigation("FavoriteMealchefs");
                });

            modelBuilder.Entity("Backend_Controller_Burhan.Models.Trainee", b =>
                {
                    b.Navigation("CookClasses");

                    b.Navigation("FavoriteChefs");

                    b.Navigation("FavoriteCourses");

                    b.Navigation("FavoriteMealTrainees");

                    b.Navigation("ProjectFiles");

                    b.Navigation("TraineeCourses");
                });

            modelBuilder.Entity("Cooking_School_ASP.NET.Models.Admin", b =>
                {
                    b.Navigation("CookClasses");

                    b.Navigation("FavoriteChefs");

                    b.Navigation("FavoriteCourses");

                    b.Navigation("FavoriteMealTrainees");

                    b.Navigation("ProjectFiles");

                    b.Navigation("TraineeCourses");
                });

            modelBuilder.Entity("Cooking_School_ASP.NET.Models.Chef", b =>
                {
                    b.Navigation("CookClasses");

                    b.Navigation("FavoriteChef");

                    b.Navigation("FavoriteChefs");

                    b.Navigation("FavoriteCourses");

                    b.Navigation("FavoriteMealTrainees");

                    b.Navigation("FavoriteMealchef");

                    b.Navigation("ProjectFiles");

                    b.Navigation("TraineeCourses");
                });
#pragma warning restore 612, 618
        }
    }
}
