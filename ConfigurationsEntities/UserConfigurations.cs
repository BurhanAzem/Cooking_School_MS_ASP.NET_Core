using Backend_Controller_Burhan.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Cooking_School_ASP.NET.Models;

namespace Cooking_School_ASP.NET.ConfigurationsEntities
{
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                new Trainee
                {
                    Id = 1,
                    FirstName = "Shady",
                    LastName = "ww",
                    Discriminator = Convert.ToString(Roles.Trainee),
                    Email = "shady@gmail.com",
                    Address = "Palestain - Nablus - Amman Street",
                    BirthDate = new DateTime(2000, 1, 1),
                    Created = DateTime.Now,
                    Level = Models.Levels.Mid,
                    PhoneNumber = 0598722898,
                    PasswordHashed = new byte[] { 0x20, 0x20 },
                    PasswordSlot = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 },
                    CardN = 94994
                },
                new Trainee
                {
                    Id = 2,
                    FirstName = "Burhan",
                    LastName = "xp",
                    Discriminator = Convert.ToString(Roles.Trainee),
                    Email = "Burhan@gmail.com",
                    Address = "Palestain - Nablus - Amman Street",
                    BirthDate = new DateTime(2000, 9, 1),
                    Created = DateTime.Now,
                    Level = Models.Levels.Mid,
                    PhoneNumber = 0598722898,
                    PasswordHashed = new byte[] { 0x20, 0x20 },
                    PasswordSlot = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 },
                    CardN = 94994
                },
                new Trainee
                {
                    Id = 3,
                    FirstName = "Bassam",
                    LastName = "kt",
                    Discriminator = Convert.ToString(Roles.Trainee),
                    Email = "Bassam@gmail.com",
                    Address = "Palestain - Nablus - Amman Street",
                    BirthDate = new DateTime(2000, 2, 1),
                    Created = DateTime.Now,
                    Level = Models.Levels.Mid,
                    PhoneNumber = 0598722898,
                    PasswordHashed = new byte[] { 0x20, 0x20 },
                    PasswordSlot = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 },
                    CardN = 94994
                },
                new Trainee
                {
                    Id = 4,
                    FirstName = "Yazan",
                    LastName = "zl",
                    Discriminator = Convert.ToString(Roles.Trainee),
                    Email = "Yazan@gmail.com",
                    Address = "Palestain - Nablus - Amman Street",
                    BirthDate = new DateTime(2001, 11, 11),
                    Created = DateTime.Now,
                    Level = Models.Levels.Mid,
                    PhoneNumber = 0598722898,
                    PasswordHashed = new byte[] { 0x20, 0x20 },
                    PasswordSlot = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 },
                    CardN = 94994,
                },
                new Trainee
                {
                    Id = 5,
                    FirstName = "sami",
                    LastName = "sh",
                    Discriminator = Convert.ToString(Roles.Trainee),
                    Email = "sami@gmail.com",
                    Address = "Palestain - Nablus - Amman Street",
                    BirthDate = new DateTime(2000, 12, 12),
                    Created = DateTime.Now,
                    Level = Models.Levels.Mid,
                    PhoneNumber = 0598722898,
                    PasswordHashed = new byte[] { 0x20, 0x20 },
                    PasswordSlot = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 },
                    CardN = 94994,
                },
                new Chef
                {
                    Id = 1,
                    FirstName = "Sultan",
                    LastName = "v",
                    Discriminator = Convert.ToString(Roles.Chef),
                    Email = "Sultan@gmail.com",
                    Address = "Italy",
                    BirthDate = new DateTime(2000, 1, 1),
                    Created = DateTime.Now,
                    PhoneNumber = 0598722898,
                    PasswordHashed = new byte[] { 0x20, 0x20 },
                    PasswordSlot = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 },
                    CvPath = "jj873y48g7h87hgf24hf49f34g4j49g9f9erhf349yf49hff34f",
                    FavoriteN = 2,
                    Salary = 7000 
                },
                new Chef
                {
                    Id = 2,
                    FirstName = "Shereen",
                    LastName = "a",
                    Discriminator = Convert.ToString(Roles.Chef),
                    Email = "Shereen@gmail.com",
                    Address = "France",
                    BirthDate = new DateTime(2000, 1, 1),
                    Created = DateTime.Now,
                    PhoneNumber = 0598722898,
                    PasswordHashed = new byte[] { 0x20, 0x20 },
                    PasswordSlot = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 },
                    CvPath = "jj873y48g7h87hgf24hf49f34g4j49g9f9erhf349yf49hff34f",
                    FavoriteN = 5,
                    Salary = 6000
                },
                new Chef
                {
                    Id = 3,
                    FirstName = "Bara",
                    LastName = "s",
                    Discriminator = Convert.ToString(Roles.Chef),
                    Email = "Bara@gmail.com",
                    Address = "Italy",
                    BirthDate = new DateTime(2000, 1, 1),
                    Created = DateTime.Now,
                    PhoneNumber = 0598722898,
                    PasswordHashed = new byte[] { 0x20, 0x20 },
                    PasswordSlot = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 },
                    CvPath = "jj873y48g7h87hgf24hf49f34g4j49g9f9erhf349yf49hff34f",
                    FavoriteN = 22,
                    Salary = 7000,
                },
                new Chef
                {
                    Id = 4,
                    FirstName = "Vatica",
                    LastName = "",
                    Discriminator = Convert.ToString(Roles.Chef),
                    Email = "Vatica@gmail.com",
                    Address = "America",
                    BirthDate = new DateTime(2000, 1, 1),
                    Created = DateTime.Now,
                    PhoneNumber = 0598722898,
                    PasswordHashed = new byte[] { 0x20, 0x20 },
                    PasswordSlot = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 },
                    CvPath = "jj873y48g7h87hgf24hf49f34g4j49g9f9erhf349yf49hff34f",
                    FavoriteN = 76,
                    Salary = 9000,
                },
                new Chef
                {
                    Id = 5,
                    FirstName = "Sosan",
                    LastName = "",
                    Discriminator = Convert.ToString(Roles.Chef),
                    Email = "Sosan@gmail.com",
                    Address = "Morocco",
                    BirthDate = new DateTime(2000, 1, 1),
                    Created = DateTime.Now,
                    PhoneNumber = 0598722898,
                    PasswordHashed = new byte[] { 0x20, 0x20 },
                    PasswordSlot = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 },
                    CvPath = "jj873y48g7h87hgf24hf49f34g4j49g9f9erhf349yf49hff34f",
                    FavoriteN = 3,
                    Salary = 4000,
                }
                );


        }
    }
}
