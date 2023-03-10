using Cooking_School_ASP.NET.Dtos.UserDto;
using System.ComponentModel.DataAnnotations;

namespace Cooking_School_ASP.NET.Dtos.ChefDto
{
    public class UpdateChefDto : UpdateUserDto
    {
        public IFormFile? Cv { get; set; }
        public decimal? Salary { get; set; }
    }
}
