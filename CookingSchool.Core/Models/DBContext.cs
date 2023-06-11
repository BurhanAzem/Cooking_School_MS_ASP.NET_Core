
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Reflection.Emit;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Net.Mime.MediaTypeNames;

namespace Cooking_School.Core.Models
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

            builder.Entity<ProjectTrainee>()
            .HasOne(x => x.Project)
            .WithMany(z => z.ProjectTrainees)
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
            .Entity<Course>()
            .Property(c => c.CourseLevel)
            .HasConversion(
            g => g.ToString(),
            g => (Levels)Enum.Parse(typeof(Levels), g));

            builder
            .Entity<ProjectTrainee>()
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
            .HasMany(t => t.ProjectTrainees)
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

            var timeOnlyConverter = new ValueConverter<TimeOnly, DateTime>(
                timeOnly => DateTime.Today.Add(timeOnly.ToTimeSpan()),
                dateTime => TimeOnly.FromDateTime(dateTime)
            );

            builder.Entity<CookClass>()
                .Property(c => c.EndingAt)
                .HasConversion(timeOnlyConverter);
            builder.Entity<CookClass>()
                .Property(c => c.StartingAt)
                .HasConversion(timeOnlyConverter);


        }
        public DbSet<User> Users { get; set; }
        public DbSet<Trainee> Trainees { get; set; }
        public DbSet<Chef> Chefs { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CookClass> CookClasses { get; set; }
        public DbSet<ApplicationT> Applications { get; set; }
        public DbSet<ClassDays> ClassDays { get; set; } 
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectFile> ProjectFiles { get; set; }
        public DbSet<ProjectTrainee> ProjectTrainees { get; set; }
        public DbSet<ProjectTraineeFile> ProjectTraineeFiles { get; set; }
        public DbSet<BlackList> BlackLists { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Trainee_Course> Trainee_Courses { get; set; }
        public DbSet<Favorite_Chef> Favorite_chefs { get; set; }
        public DbSet<Favorite_Course> Favorite_Courses { get; set; }
        public DbSet<FavoriteMeal_chef> FavoriteMeal_chefs { get; set; }
        public DbSet<FavoriteMeal_Trainee> FavoriteMeal_Trainees { get; set; }

    }
}
