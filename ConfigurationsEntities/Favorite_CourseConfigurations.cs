using Cooking_School_ASP.NET.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Cooking_School_ASP.NET.ConfigurationsEntities
{
    public class Favorite_CourseConfigurations : IEntityTypeConfiguration<Favorite_Course>
    {
        public void Configure(EntityTypeBuilder<Favorite_Course> builder)
        {
            builder.HasData(null);
        }

    }
}
