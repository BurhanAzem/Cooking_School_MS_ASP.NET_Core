using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Cooking_School.Core.Models;

namespace Cooking_School_ASP.NET.Configurations.ConfigurationsEntities
{
    public class ProjectFileConfigurations : IEntityTypeConfiguration<ProjectTraineeFile>
    {
        public void Configure(EntityTypeBuilder<ProjectTraineeFile> builder)
        {
            builder.HasData(
                new ProjectTraineeFile
                {
                    Id = 1,
                    FilePath = "uujv9e8g9839vh9h39vhrvr39vj3r9vh",
                    ProjectTraineeId = 6,
                    Created = DateTime.Now,
                },
                new ProjectTraineeFile
                {
                    Id = 2,
                    FilePath = "uujv9e8g9839vh9h39vhrvr39vj3r9vh",
                    ProjectTraineeId = 6,
                    Created = DateTime.Now,
                },
                new ProjectTraineeFile
                {
                    Id = 3,
                    FilePath = "uujv9e8g9839vh9h39vhrvr39vj3r9vh",
                    ProjectTraineeId = 6,
                    Created = DateTime.Now,
                },
                new ProjectTraineeFile
                {
                    Id = 4,
                    FilePath = "uujv9e8g9839vh9h39vhrvr39vj3r9vh",
                    ProjectTraineeId = 6,
                    Created = DateTime.Now,
                });
        }
    }
}
