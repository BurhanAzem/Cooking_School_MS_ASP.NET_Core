using Backend_Controller_Burhan.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Cooking_School.Core.Models;

namespace Cooking_School_ASP.NET.Configurations.ConfigurationsEntities
{
    public class CookClassConfigurations : IEntityTypeConfiguration<CookClass>
    {
        public void Configure(EntityTypeBuilder<CookClass> builder)
        {
            builder.HasData(
                new CookClass
                {
                    Id = 1,
                    StartingAt = new TimeOnly().AddHours(10),
                    EndingAt = new TimeOnly().AddHours(11),
                    Created = DateTime.Now,
                    ChefId = 1,
                    CourseId = 2,
                },
                new CookClass
                {
                    Id = 2,
                    StartingAt = new TimeOnly().AddHours(8),
                    EndingAt = new TimeOnly().AddHours(9),
                    Created = DateTime.Now,
                    ChefId = 1,
                    CourseId = 1,
                },
                new CookClass
                {
                    Id = 3,
                    StartingAt = new TimeOnly().AddHours(11),
                    EndingAt = new TimeOnly().AddHours(12),
                    Created = DateTime.Now,
                    ChefId = 3,
                    CourseId = 4,
                },
                new CookClass
                {
                    Id = 4,
                    StartingAt = new TimeOnly().AddHours(9),
                    EndingAt = new TimeOnly().AddHours(10),
                    Created = DateTime.Now,
                    ChefId = 2,
                    CourseId = 4,
                }
                );
        }
    }
}
