using Cooking_School_ASP.NET.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Cooking_School_ASP.NET.ConfigurationsEntities
{
    public class FavoriteMeal_TraineeConfigurations : IEntityTypeConfiguration<FavoriteMeal_Trainee>
    {
        public void Configure(EntityTypeBuilder<FavoriteMeal_Trainee> builder)
        {
            builder.HasData(null);
        }
    }
}
