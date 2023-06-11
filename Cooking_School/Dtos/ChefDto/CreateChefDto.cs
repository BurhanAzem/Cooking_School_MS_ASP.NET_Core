using Cooking_School.Dtos.UserDto;
using System.ComponentModel.DataAnnotations;

namespace Cooking_School.Dtos.ChefDto
{
    public class CreateChefDto : CreateUserDto
    {
   
        [Required]
        public IFormFile Cv { get; set; }
        [Required]
        public decimal Salary { get; set; }

    }
}
