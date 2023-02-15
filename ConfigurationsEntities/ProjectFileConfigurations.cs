using Cooking_School_ASP.NET.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Cooking_School_ASP.NET_.Models;

namespace Cooking_School_ASP.NET.ConfigurationsEntities
{
    public class ProjectFileConfigurations : IEntityTypeConfiguration<ProjectFile>
    {
        public void Configure(EntityTypeBuilder<ProjectFile> builder)
        {
            builder.HasData(
                new ProjectFile
                {
                    Id = 1,
                    ContentPath = "uujv9e8g9839vh9h39vhrvr39vj3r9vh",
                    status = status_project.submited,
                    Evalution = 90,
                    TraineeId = 6,
                    Created = DateTime.Now,
                },
                new ProjectFile
                {
                    Id = 2,
                    ContentPath = "uujv9e8g9839vh9h39vhrvr39vj3r9vh",
                    status = status_project.notSubmited,
                    TraineeId = 7,
                    Created = DateTime.Now,
                },
                new ProjectFile
                {
                    Id = 3,
                    ContentPath = "uujv9e8g9839vh9h39vhrvr39vj3r9vh",
                    status = status_project.submitedLate,
                    Evalution = 60,
                    TraineeId = 8,
                    Created = DateTime.Now,
                },
                new ProjectFile
                {
                    Id = 4,
                    ContentPath = "uujv9e8g9839vh9h39vhrvr39vj3r9vh",
                    status = status_project.submited,
                    Evalution = 75,
                    TraineeId = 9,
                    Created = DateTime.Now,
                });
        }
    } 
}
