using Cooking_School.Services.Dtos.UserDto;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Cooking_School.Services.Dtos.ChefDto
{
    public class CreateChefDto : CreateUserDto
    {
   
        [Required]
        public IFormFile Cv { get; set; }
        [Required]
        public decimal Salary { get; set; }

    }
}
