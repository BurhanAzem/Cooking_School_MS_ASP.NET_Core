using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Cooking_School.Core.Models;

namespace Cooking_School_ASP.NET.Configurations.ConfigurationsEntities
{
    public class Favorite_CourseConfigurations : IEntityTypeConfiguration<Favorite_Course>
    {
        public void Configure(EntityTypeBuilder<Favorite_Course> builder)
        {
            builder.HasData(null);
        }

    }
}
