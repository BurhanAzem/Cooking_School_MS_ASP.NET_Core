using Cooking_School_ASP.NET.Dtos.UserDto;
using System.ComponentModel.DataAnnotations;

namespace Cooking_School_ASP.NET.Dtos.ChefDto
{
    public class CreateChefDto : CreateUserDto
    {
   
        [Required]
        public IFormFile Cv { get; set; }
        [Required]
        public decimal Salary { get; set; }

    }
}
