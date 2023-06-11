using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Backend_Controller_Burhan.Models;
using Cooking_School.Core.Models;

namespace Cooking_School_ASP.NET.Configurations.ConfigurationsEntities
{
    public class TraineeConfigurations : IEntityTypeConfiguration<Trainee>
    {
        public void Configure(EntityTypeBuilder<Trainee> builder)
        {
            builder.HasData(
                new Trainee
                {
                    Id = 6,
                    FirstName = "Shady",
                    LastName = "",
                    Email = "shady@gmail.com",
                    Address = "Palestain - Nablus - Amman Street",
                    BirthDate = new DateTime(2000, 1, 1),
                    Created = DateTime.Now,
                    Level = Levels.intermediate,
                    PhoneNumber = 0598722898,
                    PasswordHashed = new byte[] { 0x20, 0x20 },
                    PasswordSlot = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 },
                    CardN = 94994,
                    Discriminator = Convert.ToString(Roles.Trainee)
                },
                new Trainee
                {
                    Id = 7,
                    FirstName = "Burhan",
                    LastName = "",
                    Email = "Burhan@gmail.com",
                    Address = "Palestain - Nablus - Amman Street",
                    BirthDate = new DateTime(2000, 9, 1),
                    Created = DateTime.Now,
                    Level = Levels.intermediate,
                    PhoneNumber = 0598722898,
                    PasswordHashed = new byte[] { 0x20, 0x20 },
                    PasswordSlot = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 },
                    CardN = 94994,
                    Discriminator = Convert.ToString(Roles.Trainee)
                },
                new Trainee
                {
                    Id = 8,
                    FirstName = "Bassam",
                    LastName = "",
                    Email = "Bassam@gmail.com",
                    Address = "Palestain - Nablus - Amman Street",
                    BirthDate = new DateTime(2000, 2, 1),
                    Created = DateTime.Now,
                    Level = Levels.intermediate,
                    PhoneNumber = 0598722898,
                    PasswordHashed = new byte[] { 0x20, 0x20 },
                    PasswordSlot = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 },
                    CardN = 94994,
                    Discriminator = Convert.ToString(Roles.Trainee)
                },
                new Trainee
                {
                    Id = 9,
                    FirstName = "Yazan",
                    LastName = "",
                    Email = "Yazan@gmail.com",
                    Address = "Palestain - Nablus - Amman Street",
                    BirthDate = new DateTime(2001, 11, 11),
                    Created = DateTime.Now,
                    Level = Levels.intermediate,
                    PhoneNumber = 0598722898,
                    PasswordHashed = new byte[] { 0x20, 0x20 },
                    PasswordSlot = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 },
                    CardN = 94994,
                    Discriminator = Convert.ToString(Roles.Trainee)
                },
                new Trainee
                {
                    Id = 10,
                    FirstName = "sami",
                    LastName = "",
                    Email = "sami@gmail.com",
                    Address = "Palestain - Nablus - Amman Street",
                    BirthDate = new DateTime(2000, 12, 12),
                    Created = DateTime.Now,
                    Level = Levels.intermediate,
                    PhoneNumber = 0598722898,
                    PasswordHashed = new byte[] { 0x20, 0x20 },
                    PasswordSlot = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 },
                    CardN = 94994,
                    Discriminator = Convert.ToString(Roles.Trainee)
                }
                ); ;
        }

    }
}
