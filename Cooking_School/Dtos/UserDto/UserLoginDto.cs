using System.ComponentModel.DataAnnotations;

namespace Cooking_School.Dtos.UserDto
{
    public class UserLoginDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
