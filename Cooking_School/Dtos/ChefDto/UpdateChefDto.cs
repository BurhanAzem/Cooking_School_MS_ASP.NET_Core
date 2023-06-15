using Cooking_School.Dtos.UserDto;
using System.ComponentModel.DataAnnotations;

namespace Cooking_School.Dtos.ChefDto
{
    public class UpdateChefDto : UpdateUserDto
    {
        public IFormFile? Cv { get; set; }
        public decimal? Salary { get; set; }
    }
}
