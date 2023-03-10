﻿using Azure;
using Backend_Controller_Burhan.Models;
using Cooking_School_ASP.NET.ConfigurationsEntities;
using Cooking_School_ASP.NET.Controllers.AdminControllers;
using Cooking_School_ASP.NET.Models;
using Cooking_School_ASP.NET_.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Reflection.Emit;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Net.Mime.MediaTypeNames;

namespace Cooking_School_ASP.NET.Models
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions options) : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //builder.ApplyConfiguration(new UserConfigurations());
            //builder.ApplyConfiguration(new TraineeConfigurations());
            //builder.ApplyConfiguration(new ChefConfigurations());
            //builder.ApplyConfiguration(new CourseConfigurations());
            //builder.ApplyConfiguration(new CookClassConfigurations());
            //builder.ApplyConfiguration(new ProjectConfigurations());
            //builder.ApplyConfiguration(new ProjectFileConfigurations());

            builder.Entity<User>()
                .HasDiscriminator<string>("Discriminator")
                .HasValue<Trainee>("Trainee")
                .HasValue<Chef>("Chef")
                .HasValue<Admin>("Administrator");

            builder.Entity<Favorite_Chef>()
            .HasOne(x => x.Chef)
            .WithMany(z => z.FavoriteChef)
            .HasForeignKey(x => x.ChefId).
            OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Favorite_Chef>()
            .HasOne(x => x.Trainee)
            .WithMany(z => z.FavoriteChefs)
            .HasForeignKey(x => x.TraineeId).
            OnDelete(DeleteBehavior.Restrict);


            builder.Entity<Favorite_Course>()
            .HasOne(x => x.Trainee)
            .WithMany(z => z.FavoriteCourses)
            .HasForeignKey(x => x.TraineeId).
            OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Favorite_Course>()
            .HasOne(x => x.Course)
            .WithMany(z => z.FavoriteCourse)
            .HasForeignKey(x => x.CourseId).
            OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ApplicationT>()
            .HasOne(x => x.CookClass)
            .WithMany(z => z.Applications)
            .HasForeignKey(x => x.CookClassId).
            OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ProjectFile>()
            .HasOne(x => x.Project)
            .WithMany(z => z.projectFiles)
            .HasForeignKey(x => x.ProjectId).
            OnDelete(DeleteBehavior.Restrict);


            builder
           .Entity<ClassDays>()
            .Property(c => c.Day)
            .HasConversion(
            g => g.ToString(),
            g => (WeekDays)Enum.Parse(typeof(WeekDays), g));

            builder
            .Entity<Trainee>()
            .Property(c => c.Level)
            .HasConversion(
            g => g.ToString(),
            g => (Levels)Enum.Parse(typeof(Levels), g));

            builder
            .Entity<ProjectFile>()
            .Property(c => c.status)
            .HasConversion(
            g => g.ToString(),
            g => (status_project)Enum.Parse(typeof(status_project), g));

            builder
            .Entity<ApplicationT>()
            .Property(c => c.status)
            .HasConversion(
            g => g.ToString(),
            g => (status_apply)Enum.Parse(typeof(status_apply), g));

            builder.Entity<Trainee>()
            .HasMany(t => t.FavoriteChefs)
            .WithOne(t => t.Trainee)
            .HasForeignKey(t => t.TraineeId);
            builder.Entity<Trainee>()
            .HasMany(t => t.TraineeCourses)
            .WithOne(t => t.Trainee)
            .HasForeignKey(t => t.TraineeId);
            builder.Entity<Trainee>()
            .HasMany(t => t.FavoriteMealTrainees)
            .WithOne(t => t.Trainee)
            .HasForeignKey(t => t.TraineeId);
            builder.Entity<Trainee>()
            .HasMany(t => t.ProjectFiles)
            .WithOne(t => t.Trainee)
            .HasForeignKey(t => t.TraineeId);
            builder.Entity<Trainee>()
            .HasMany(t => t.FavoriteCourses)
            .WithOne(t => t.Trainee)
            .HasForeignKey(t => t.TraineeId);

            builder.Entity<Chef>()
            .HasMany(t => t.CookClasses)
            .WithOne(t => t.Chef)
            .HasForeignKey(t => t.ChefId);
            builder.Entity<Chef>()
            .HasMany(t => t.FavoriteMealchef)
            .WithOne(t => t.Chef)
            .HasForeignKey(t => t.ChefId);
            builder.Entity<Chef>()
            .HasMany(t => t.FavoriteChef)
            .WithOne(t => t.Chef)
            .HasForeignKey(t => t.ChefId);

            builder.Entity<User>().ToTable("User");
            builder.Entity<Course>().ToTable("Course");
            builder.Entity<ProjectFile>().ToTable("ProjectFile");
            builder.Entity<Project>().ToTable("Project");
            builder.Entity<Trainee_Course>().ToTable("Trainee_Course");
            builder.Entity<FavoriteMeal_Trainee>().ToTable("FavoriteMeal_Trainee");
            builder.Entity<Favorite_Course>().ToTable("Favorite_Course");
            builder.Entity<FavoriteMeal_chef>().ToTable("FavoriteMeal_chef");
            builder.Entity<Favorite_Chef>().ToTable("Favorite_chef");
            builder.Entity<ApplicationT>().ToTable("Application");
            builder.Entity<ClassDays>().ToTable("ClassDays");
            builder.Entity<CookClass>().ToTable("CookClass");
        }
        public DbSet<User> Users { get; set; }
        //public DbSet<Trainee> Trainees { get; set; }
        //public DbSet<Chef> Chefs { get; set; }
        //public DbSet<Admin> Admins { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CookClass> CookClasses { get; set; }
        public DbSet<ApplicationT> Applications { get; set; }
        public DbSet<ClassDays> ClassDays { get; set; } 
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectFile> ProjectFiles { get; set; }
        public DbSet<BlackList> BlackLists { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Trainee_Course> Trainee_Courses { get; set; }
        public DbSet<Favorite_Chef> Favorite_chefs { get; set; }
        public DbSet<Favorite_Course> Favorite_Courses { get; set; }
        public DbSet<FavoriteMeal_chef> FavoriteMeal_chefs { get; set; }
        public DbSet<FavoriteMeal_Trainee> FavoriteMeal_Trainees { get; set; }

    }
}


//protected override void OnConfiguring(DbContextOptionsBuilder options)
//=> options.UseSqlite(@"Data Source = D:\Temp\Demo.db");


//modelBuilder.Entity<ChefTrainee>().HasKey(ap => new { ap.username, ap.slug });
//modelBuilder.Entity<Tag>().HasKey(t => new { t.tag, t.articleslug });


////modelBuilder.Entity<ProfileFollow>().HasKey(pf => new { pf.usernamefollower, pf.usernamefollow });
////modelBuilder
////.Entity<Tag>(
////    eb =>
////    {
////        eb.HasNoKey();
////    });

//modelBuilder.Entity<Course>()
//.HasOne<Trainee>(p => p.User)
//.WithOne(ad => ad.profile)
//.HasForeignKey<Trainee>(u => u.profileusername);

//modelBuilder.Entity<Article>()
//.HasOne<Course>(p => p.author)
//.WithOne(a => a.article)
//.HasForeignKey<Course>(p => p.articleslug);

//modelBuilder.Entity<Course>()
//.HasMany(c => c.ProfileFollowing)
//.WithMany(c => c.ProfileFolloweres);

//modelBuilder.Entity<Article>()
//.HasMany<Tag>(a => a.tagList)
//    .WithOne(t => t.article);
//modelBuilder.Entity<Article>().Navigation(a => a.author).AutoInclude();
//modelBuilder.Entity<Course>().Navigation(p => p.article).AutoInclude();
////delBuilder.Entity<Article>().Navigation(a => a.favorite).AutoInclude();
////odelBuilder.Entity<Article>().Navigation(a => a.comment).AutoInclude();
//modelBuilder.Entity<Trainee>().Navigation(u => u.profile).AutoInclude();
