using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Cooking_School.Core.Models;

namespace Cooking_School_ASP.NET.Configurations.ConfigurationsEntities
{
    public class Favorite_ChefConfigurations : IEntityTypeConfiguration<Favorite_Chef>
    {
        public void Configure(EntityTypeBuilder<Favorite_Chef> builder)
        {
            builder.HasData(null);
        }

    }
}
