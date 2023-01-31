using Cooking_School_ASP.NET.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cooking_School_ASP.NET.ConfigurationsEntities
{
    public class FavoriteMeal_ChefConfigurations : IEntityTypeConfiguration<FavoriteMeal_chef>
    {
        public void Configure(EntityTypeBuilder<FavoriteMeal_chef> builder)
        {
            builder.HasData(null);
        }

    }
}
