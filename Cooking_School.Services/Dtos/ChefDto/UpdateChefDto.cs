using Cooking_School.Services.Dtos.UserDto;
using Microsoft.AspNetCore.Http;

namespace Cooking_School.Services.Dtos.ChefDto
{
    public class UpdateChefDto : UpdateUserDto
    {
        public IFormFile? Cv { get; set; }
        public decimal? Salary { get; set; }
    }
}
