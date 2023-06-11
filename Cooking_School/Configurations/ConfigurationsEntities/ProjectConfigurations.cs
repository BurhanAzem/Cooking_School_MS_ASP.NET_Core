using Backend_Controller_Burhan.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Cooking_School.Core.Models;

namespace Cooking_School_ASP.NET.Configurations.ConfigurationsEntities
{
    public class ProjectConfigurations : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasData(
                new Project
                {
                    Id = 1,
                    ProjectName = "SaltUp",
                    Description = "avg taste on food",
                    Created = DateTime.Now,
                    ExpirDate = new DateTime(2023, 2, 20, 11, 59, 59),
                    CookClassId = 2
                },
                new Project
                {
                    Id = 2,
                    ProjectName = "ShokerUp",
                    Description = "avg taste on food",
                    Created = DateTime.Now,
                    ExpirDate = new DateTime(2023, 2, 20, 11, 59, 59),
                    CookClassId = 1
                },
                new Project
                {
                    Id = 3,
                    ProjectName = "HotFlag",
                    Description = "avg taste on food",
                    Created = DateTime.Now,
                    ExpirDate = new DateTime(2023, 2, 20, 11, 59, 59),
                    CookClassId = 1
                },
                new Project
                {
                    Id = 4,
                    ProjectName = "Mexeco",
                    Description = "avg taste on food",
                    Created = DateTime.Now,
                    ExpirDate = new DateTime(2023, 2, 20, 11, 59, 59),
                    CookClassId = 4
                }
                );
        }
    }
}
