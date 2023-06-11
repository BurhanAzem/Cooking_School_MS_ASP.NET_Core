using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Cooking_School.Core.Models;

namespace Cooking_School_ASP.NET.Configurations.ConfigurationsEntities
{
    public class ApplicationConfigurations : IEntityTypeConfiguration<ApplicationT>
    {
        public void Configure(EntityTypeBuilder<ApplicationT> builder)
        {
            builder.HasData(null);
        }

    }
}
