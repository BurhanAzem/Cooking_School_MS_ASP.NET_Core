using Cooking_School_ASP.NET.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Cooking_School_ASP.NET.ConfigurationsEntities
{
    public class ClassDaysConfigurations : IEntityTypeConfiguration<ClassDays>
    {
        public void Configure(EntityTypeBuilder<ClassDays> builder)
        {
            builder.HasData(
                new ClassDays
                {
                    Id = 1,
                    CookClassId = 1,
                    Day = WeekDays.Monday
                },
                new ClassDays
                {
                    Id = 2,
                    CookClassId = 1,
                    Day = WeekDays.Wednesday
                },
                new ClassDays
                {
                    Id = 3,
                    CookClassId = 1,
                    Day = WeekDays.Monday
                },
                new ClassDays
                {
                    Id = 4,
                    CookClassId = 2,
                    Day = WeekDays.Monday
                },
                new ClassDays
                {
                    Id = 5,
                    CookClassId = 2,
                    Day = WeekDays.Monday
                },
                new ClassDays
                {
                    Id = 6,
                    CookClassId = 2,
                    Day = WeekDays.Monday
                }
                );
        }
    }
}
