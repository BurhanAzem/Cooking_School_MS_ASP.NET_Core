using Cooking_School_ASP.NET.Models;
using System.ComponentModel.DataAnnotations;

namespace Cooking_School_ASP.NET.Dtos.UserDto
{
    public class CreateUserDto
    {
        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "Name Is Too Long")]
        public string FirstName { get; set; }
        [StringLength(maximumLength: 50, ErrorMessage = "Name Is Too Long")]
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Discriminator { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "Address Is Too Long")]
        public string Address { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.PhoneNumber)]
        public int PhoneNumber { get; set; }
    }
}
