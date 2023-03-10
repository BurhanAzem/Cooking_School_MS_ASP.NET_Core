using Cooking_School_ASP.NET.Dtos.UserDto;
using Cooking_School_ASP.NET.Models;
using System.ComponentModel.DataAnnotations;

namespace Cooking_School_ASP.NET.Dtos.TraineeDto
{
    public class CreateTraineeDto : CreateUserDto
    {

        [Required]
        public string Level { get; set; }
        [Required]
        [DataType(DataType.CreditCard)]
        public int CardN { get; set; }
        public IFormFile? image { get; set; }

    }
}
