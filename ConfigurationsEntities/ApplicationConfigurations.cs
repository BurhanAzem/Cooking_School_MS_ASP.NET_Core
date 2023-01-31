using Cooking_School_ASP.NET.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Cooking_School_ASP.NET.ConfigurationsEntities
{
    public class ApplicationConfigurations : IEntityTypeConfiguration<ApplicationT>
    {
        public void Configure(EntityTypeBuilder<ApplicationT> builder)
        {
            builder.HasData(null); 
        }
    
    }
}
