using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Backend_Controller_Burhan.Models;
using Cooking_School.Core.Models;

namespace Cooking_School_ASP.NET.Configurations.ConfigurationsEntities
{
    public class CourseConfigurations : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasData(
                new Course
                {
                    Id = 1,
                    CourseName = "CC1_Pizza",
                    Price = 100,
                    FavoriteN = 21,
                    Created = DateTime.Now,
                    Description = "become able to cook pizza",
                },
                new Course
                {
                    Id = 2,
                    CourseName = "CC1_Traditional",
                    Price = 80,
                    FavoriteN = 11,
                    Created = DateTime.Now,
                    Description = "become able to cook traditional",
                },
                new Course
                {
                    Id = 3,
                    CourseName = "CC1_Sushi",
                    Price = 200,
                    FavoriteN = 8,
                    Created = DateTime.Now,
                    Description = "become able to cook sushi",
                },
                new Course
                {
                    Id = 4,
                    CourseName = "CC1_Fish",
                    Price = 150,
                    FavoriteN = 18,
                    Created = DateTime.Now,
                    Description = "become able to cook Fish",
                },
                new Course
                {
                    Id = 5,
                    CourseName = "CC1_Sweet",
                    Price = 100,
                    FavoriteN = 30,
                    Created = DateTime.Now,
                    Description = "become able to cook Sweet",
                });

        }
    }
}
