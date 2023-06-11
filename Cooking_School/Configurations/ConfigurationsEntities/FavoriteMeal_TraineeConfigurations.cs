using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Cooking_School.Core.Models;

namespace Cooking_School_ASP.NET.Configurations.ConfigurationsEntities
{
    public class FavoriteMeal_TraineeConfigurations : IEntityTypeConfiguration<FavoriteMeal_Trainee>
    {
        public void Configure(EntityTypeBuilder<FavoriteMeal_Trainee> builder)
        {
            builder.HasData(null);
        }
    }
}
