using Cooking_School.Services.Dtos.UserDto;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Cooking_School.Services.Dtos.TraineeDto
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
