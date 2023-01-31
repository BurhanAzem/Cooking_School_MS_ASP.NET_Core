using Backend_Controller_Burhan.Models;
using Cooking_School_ASP.NET.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cooking_School_ASP.NET.ConfigurationsEntities
{
    public class ChefConfigurations : IEntityTypeConfiguration<Chef>
    {
        public void Configure(EntityTypeBuilder<Chef> builder)
        {
            builder.HasData(
                new Chef
                {
                    Id = 1,
                    FirstName = "Sultan",
                    LastName = "",
                    Email = "Sultan@gmail.com",
                    Address = "Italy",
                    BirthDate = new DateTime(2000, 1, 1),
                    Created = DateTime.Now,
                    PhoneNumber = 0598722898,
                    PasswordHashed = new byte[] { 0x20, 0x20 },
                    PasswordSlot = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 },
                    Cv = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 },
                    FavoriteN = 2,
                    Salary = 7000,
                    Discriminator = "Chef"
                },
                new Chef
                {
                    Id = 2,
                    FirstName = "Shereen",
                    LastName = "",
                    Email = "Shereen@gmail.com",
                    Address = "France",
                    BirthDate = new DateTime(2000, 1, 1),
                    Created = DateTime.Now,
                    PhoneNumber = 0598722898,
                    PasswordHashed = new byte[] { 0x20, 0x20 },
                    PasswordSlot = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 },
                    Cv = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 },
                    FavoriteN = 5,
                    Salary = 6000,
                    Discriminator = "Chef"
                },
                new Chef
                {
                    Id = 3,
                    FirstName = "Bara",
                    LastName = "",
                    Email = "Bara@gmail.com",
                    Address = "Italy",
                    BirthDate = new DateTime(2000, 1, 1),
                    Created = DateTime.Now,
                    PhoneNumber = 0598722898,
                    PasswordHashed = new byte[] { 0x20, 0x20 },
                    PasswordSlot = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 },
                    Cv = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 },
                    FavoriteN = 22,
                    Salary = 7000,
                    Discriminator = "Chef"
                },
                new Chef
                {
                    Id = 4,
                    FirstName = "Vatica",
                    LastName = "",
                    Email = "Vatica@gmail.com",
                    Address = "America",
                    BirthDate = new DateTime(2000, 1, 1),
                    Created = DateTime.Now,
                    PhoneNumber = 0598722898,
                    PasswordHashed = new byte[] { 0x20, 0x20 },
                    PasswordSlot = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 },
                    Cv = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 },
                    FavoriteN = 76,
                    Salary = 9000,
                    Discriminator = "Chef"
                },
                new Chef
                {
                    Id = 5,
                    FirstName = "Sosan",
                    LastName = "",
                    Email = "Sosan@gmail.com",
                    Address = "Morocco",
                    BirthDate = new DateTime(2000, 1, 1),
                    Created = DateTime.Now,
                    PhoneNumber = 0598722898,
                    PasswordHashed = new byte[] { 0x20, 0x20 },
                    PasswordSlot = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 },
                    Cv = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 },
                    FavoriteN = 3,
                    Salary = 4000,
                    Discriminator = "Chef"
                }
                ) ;
        }
    }
}
