using System.ComponentModel.DataAnnotations;

namespace Cooking_School_ASP.NET.Dtos.UserDto
{
    public class UpdateUserDto
    {
        [StringLength(maximumLength: 50, ErrorMessage = "Name Is Too Long")]
        public string? FirstName { get; set; }
        [StringLength(maximumLength: 50, ErrorMessage = "Name Is Too Long")]

        public string? LastName { get; set; }

        public DateTime BirthDate { get; set; }

        [StringLength(maximumLength: 50, ErrorMessage = "Address Is Too Long")]
        public string? Address { get; set; }

        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [DataType(DataType.PhoneNumber)]
        public int? PhoneNumber { get; set; }
    }
}

