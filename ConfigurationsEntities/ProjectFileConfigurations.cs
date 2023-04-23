using Cooking_School_ASP.NET.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Cooking_School_ASP.NET_.Models;

namespace Cooking_School_ASP.NET.ConfigurationsEntities
{
    public class ProjectFileConfigurations : IEntityTypeConfiguration<SubmitedFile>
    {
        public void Configure(EntityTypeBuilder<SubmitedFile> builder)
        {
            builder.HasData(
                new SubmitedFile
                {
                    Id = 1,
                    ContentPath = "uujv9e8g9839vh9h39vhrvr39vj3r9vh",
                    status = status_project.submited,
                    Evalution = 90,
                    TraineeId = 6,
                    Created = DateTime.Now,
                },
                new SubmitedFile
                {
                    Id = 2,
                    ContentPath = "uujv9e8g9839vh9h39vhrvr39vj3r9vh",
                    status = status_project.notSubmited,
                    TraineeId = 7,
                    Created = DateTime.Now,
                },
                new SubmitedFile
                {
                    Id = 3,
                    ContentPath = "uujv9e8g9839vh9h39vhrvr39vj3r9vh",
                    status = status_project.submitedLate,
                    Evalution = 60,
                    TraineeId = 8,
                    Created = DateTime.Now,
                },
                new SubmitedFile
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
